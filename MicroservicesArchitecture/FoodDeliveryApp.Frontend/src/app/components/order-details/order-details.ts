import { Component, inject, signal, OnInit } from '@angular/core';
import { Order } from '../../models/order.type';
import { OrderItem } from '../../models/orderItem.type';
import { OrderService } from '../../services/order-service';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin, of, switchMap } from 'rxjs';
import { RestaurantService } from '../../services/restaurant-service';
import { DatePipe, CommonModule } from '@angular/common';

@Component({
  selector: 'app-order-details',
  imports: [DatePipe, CommonModule],
  templateUrl: './order-details.html',
  styleUrl: './order-details.scss'
})
export class OrderDetails implements OnInit {
  private orderService = inject(OrderService);
  private restaurantService = inject(RestaurantService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  order = signal<Order | null>(null);
  orderItems = signal<Array<OrderItem>>([]);
  restaurantName = signal<string>('...');

  ngOnInit(): void 
  {
    this.route.paramMap.pipe(
      switchMap(params => {
        const id = params.get('id');
        if(id)
        {
          return forkJoin({
            order: this.orderService.getOrderById(id),
            orderItems: this.orderService.getOrderItemsByOrderId(id)
          });
        }
        return of({ order: null, orderItems: [] });
    })
    ).subscribe({
      next: ({order, orderItems}) => {
        const restaurantId = order?.restaurantId;
        if (restaurantId != null) {
          this.getRestaurantName(restaurantId);
        }
        this.order.set(order),
        this.orderItems.set(orderItems)
      },
      error: (err) => {
        this.router.navigate(['/error'], { state: { error: err.error} });
      }
    });
  }

  getRestaurantName(id: number) {
    this.restaurantService.getRestaurantName(id).subscribe({
      next: (name) => {
        this.restaurantName.set(name);
      },
      error: (err) => {
        this.router.navigate(['/error'], { state: { error: err.error} });
      }
    });
  }
}

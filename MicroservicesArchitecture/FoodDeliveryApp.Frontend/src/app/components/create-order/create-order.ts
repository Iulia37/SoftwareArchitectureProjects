import { Component, inject, input, computed, effect } from '@angular/core';
import { OrderRequest } from '../../models/orderRequest.type';
import { OrderService } from '../../services/order-service';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth-service';
import { Router } from '@angular/router';
import { Order } from '../../models/order.type';

@Component({
  selector: 'app-create-order',
  imports: [CommonModule],
  templateUrl: './create-order.html',
  styleUrl: './create-order.scss'
})
export class CreateOrder {
  private orderService = inject(OrderService);
  private router = inject(Router);
  authService = inject(AuthService);

  restaurantId = input();
  readonly orderItems = this.orderService.orderItems;
  totalPrice = computed(() => this.orderItems().reduce(
    (total, item) => total + item.quantity * item.unitPrice, 0));
  
  restaurantEffect = effect(() => {
    const id = this.restaurantId();
    if (id) {
      this.orderService.clearOrderItems();
    }
  });

  submitOrder() {
    const orderRequest: OrderRequest = {
        userId: String(this.authService.getUserId()),
        restaurantId: Number(this.restaurantId()),
        orderItems: this.orderItems()
    }
    this.orderService.createOrder(orderRequest).subscribe({
      next: (createdOrder: Order) => {
        this.router.navigate(['/payment'], { queryParams: { orderId: createdOrder.id}});
      },
      error: (err) => {
        this.router.navigate(['/error'], { state: { error: err.error} });
      }
    });
  }
  
}
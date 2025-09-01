import { Component, inject, OnInit, signal } from '@angular/core';
import { AuthService } from '../../services/auth-service';
import { ActivatedRoute, Router } from '@angular/router';
import { of, switchMap, forkJoin } from 'rxjs';
import { UserService } from '../../services/user-service';
import { User } from '../../models/user.type';
import { OrderService } from '../../services/order-service';
import { Order } from '../../models/order.type';
import { OrderItem } from '../order-item/order-item';

@Component({
  selector: 'app-user-details',
  imports: [OrderItem],
  templateUrl: './user-details.html',
  styleUrl: './user-details.scss'
})
export class UserDetails implements OnInit {
  private userService = inject(UserService);
  private orderService = inject(OrderService);
  private route = inject(ActivatedRoute); 
  private router = inject(Router);

  authService = inject(AuthService);
  user = signal<User | null>(null);
  userOrders = signal<Order[]>([]);

  ngOnInit(): void {
    this.route.paramMap.pipe(
      switchMap(params => {
        const id = params.get('id');
        if (id) {
          return forkJoin({
            user: this.userService.getUserById(id),
            orders: this.orderService.getOrdersByUserId(id)
          });
        }
        return of({ user: null, orders: [] });
      })
    ).subscribe({
      next: ({ user, orders }) => {
        this.user.set(user);
        this.userOrders.set(orders);
      },
      error: (err) => {
        this.router.navigate(['/error'], { state: { error: err.error } });
      }
    });
  }
}

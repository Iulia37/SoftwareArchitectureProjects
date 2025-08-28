import { Component, inject, input, computed, effect } from '@angular/core';
import { OrderRequest } from '../../models/orderRequest.type';
import { OrderService } from '../../services/order-service';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth-service';

@Component({
  selector: 'app-order-component',
  imports: [CommonModule],
  templateUrl: './order-component.html',
  styleUrl: './order-component.scss'
})
export class OrderComponent {
  private orderService = inject(OrderService);
  private authService = inject(AuthService);

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
    this.orderService.createOrder(orderRequest);
  }
  
}
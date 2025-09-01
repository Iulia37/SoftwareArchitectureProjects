import { Component, input, inject } from '@angular/core';
import { DatePipe } from '@angular/common';
import { Order } from '../../models/order.type';
import { Router } from '@angular/router';

@Component({
  selector: 'app-order-item',
  imports: [DatePipe],
  templateUrl: './order-item.html',
  styleUrl: './order-item.scss'
})
export class OrderItem {
  private router = inject(Router);

  order = input.required<Order>();

  orderDetails() {
    this.router.navigate(['/order', this.order().id]);
  }
}

import { Component, inject, OnInit, signal } from '@angular/core';
import { Payment } from '../../models/payment.type';
import { Order } from '../../models/order.type';
import { PaymentService } from '../../services/payment-service';
import { OrderService } from '../../services/order-service';
import { Router, ActivatedRoute } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-payment-component',
  imports: [FormsModule, CommonModule],
  templateUrl: './payment-component.html',
  styleUrl: './payment-component.scss'
})
export class PaymentComponent implements OnInit{
  private paymentService = inject(PaymentService);
  private orderService = inject(OrderService);
  private route = inject(ActivatedRoute);

  order = signal<Order | null>(null);
  paymentMethod: string = 'card';
  cardDetails = { number: '', expiry: '', cvv: '' };
  loading = false;
  message = '';
  address: string = '';

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      const orderId = params['orderId'];
      this.orderService.getOrderById(orderId).subscribe(o => {
        this.order.set(o);
      });
    });
  }

  pay() {
    if(this.order() == null)
    {
      this.message = "Order not found!";
      return;
    }
    this.loading = true;
    this.paymentService.createPayment({
      id: 0,
      orderId: this.order()!.id,
      userId: this.order()!.userId,
      amount: this.order()!.totalPrice,
      method: this.paymentMethod,
      address: this.address,
      createdAt: new Date()
    }).subscribe({
        next: () => {
          this.loading = false;
          this.message = 'Payment completed successfully!';
        },
        error: () => {
          this.loading = false;
          this.message = 'Payment error!';
        }
    });  
  }
}

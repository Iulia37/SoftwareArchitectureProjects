import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Payment } from '../models/payment.type';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {
  private http = inject(HttpClient);
  private apiUrl = 'https://localhost:7290/api/Payments';

  getPaymentById = (id: string) => {
    const url = `${this.apiUrl}/${id}`;
    return this.http.get<Payment>(url);
  }

  getPaymentsByUserId = (userId: string) => {
    const url = `${this.apiUrl}/user/${userId}`;
    return this.http.get<Array<Payment>>(url);
  }

  createPayment = (payment: Payment) => {
    return this.http.post(this.apiUrl, payment);
  }

  updatePayment = (payment: Payment) => {
    const url = `${this.apiUrl}/${payment.id}`;
    return this.http.put(url, payment);
  }

  deletePayment = (payment: Payment) => {
    const url = `${this.apiUrl}/${payment.id}`;
    return this.http.delete(url);
  }
}

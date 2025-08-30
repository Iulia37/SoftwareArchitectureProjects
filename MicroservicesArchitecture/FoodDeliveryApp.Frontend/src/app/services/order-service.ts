import { Injectable, inject, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Order } from '../models/order.type'
import { OrderItem } from '../models/orderItem.type'
import { OrderRequest } from '../models/orderRequest.type'

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private http = inject(HttpClient);
  private apiUrl = 'https://localhost:7122/api/Orders';

  getOrdersByUserId = (id: string) => {
    const url = `${this.apiUrl}/user/${id}`;
    return this.http.get<Array<Order>>(url);
  }

  getOrderById = (id: string) => {
    const url = `${this.apiUrl}/${id}`;
    return this.http.get<Order>(url);
  }

  createOrder = (order: OrderRequest) => {
    return this.http.post<Order>(this.apiUrl, order);
  }

  updateOrder = (order: Order) => {
    const url = `${this.apiUrl}/${order.id}`;
    return this.http.put<Order>(url, order);
  }

  deleteOrder = (id: string) => {
    const url = `${this.apiUrl}/${id}`;
    return this.http.delete(url);
  }


  private _orderItems = signal<OrderItem[]>([]);
  readonly orderItems = this._orderItems.asReadonly();

  addOrUpdateOrderItem(item: OrderItem) {
    this._orderItems.update(items => {
      const idx = items.findIndex(i => i.menuItemId === item.menuItemId);
      if (item.quantity === 0) {
        return items.filter(i => i.menuItemId !== item.menuItemId);
      }
      if (idx >= 0) {
        return items.map(i => i.menuItemId === item.menuItemId ? item : i);
      }
      return [...items, item];
    });
  }

  clearOrderItems() {
    this._orderItems.set([]);
  }

}

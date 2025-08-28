import { Component, signal, input, inject, computed } from '@angular/core';
import { MenuItem } from '../../models/menu-item.type';
import { OrderItem } from '../../models/orderItem.type';
import { OrderService } from '../../services/order-service';

@Component({
  selector: 'app-counter',
  imports: [],
  templateUrl: './counter.html',
  styleUrl: './counter.scss'
})
export class Counter {
  private orderService = inject(OrderService);

  count = signal<number>(0);
  menuItem = input<MenuItem>();
  orderItem = computed<(OrderItem & { name?: string }) | null>(() => {
    const menuItem = this.menuItem();
    if (!menuItem) return null;
    return {
      id: 0,
      menuItemId: menuItem.id,
      menuItemName: menuItem.name,
      orderId: 0,
      quantity: this.count(),
      unitPrice: menuItem.price
    };
  });

  increaseCount()
  {
    this.count.update(c => c + 1);
    const item = this.orderItem();
    if(item)
    {
      this.orderService.addOrUpdateOrderItem(item);
    }
  }

  decreaseCount()
  {
    this.count.update(c => Math.max(c - 1, 0));
    const item = this.orderItem();
    if(item) 
    {
      this.orderService.addOrUpdateOrderItem(item);
    }
  }
}

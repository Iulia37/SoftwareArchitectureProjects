import { OrderItem } from '../models/orderItem.type';

export type OrderRequest = {
  userId: string;
  restaurantId: number;
  orderItems: OrderItem[];
};
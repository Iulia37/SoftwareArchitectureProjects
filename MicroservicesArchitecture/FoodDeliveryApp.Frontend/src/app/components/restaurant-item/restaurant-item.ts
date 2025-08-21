import { Component, input } from '@angular/core';
import { Restaurant } from '../../models/restaurant.type'

@Component({
  selector: 'app-restaurant-item',
  imports: [],
  templateUrl: './restaurant-item.html',
  styleUrl: './restaurant-item.scss'
})
export class RestaurantItem {
  restaurant = input.required<Restaurant>();
}

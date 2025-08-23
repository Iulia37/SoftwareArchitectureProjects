import { Component, input, inject } from '@angular/core';
import { Restaurant } from '../../models/restaurant.type'
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { RestaurantService } from '../../services/restaurant-service';

@Component({
  selector: 'app-restaurant-item',
  imports: [ CommonModule ],
  templateUrl: './restaurant-item.html',
  styleUrl: './restaurant-item.scss'
})
export class RestaurantItem {
  restaurant = input.required<Restaurant>();
  private router = inject(Router);
  private restaurantervice = inject(RestaurantService);

  onRestaurantClick(): void {
    this.router.navigate(['/restaurant', this.restaurant().id]);
  }

  getImageUrl()
  {
    return this.restaurantervice.getImageUrl(this.restaurant().imageUrl)
  }
}

import { Component, inject, signal, OnInit } from '@angular/core';
import { RestaurantService } from '../../services/restaurant-service';
import { Router } from '@angular/router';
import { Restaurant } from '../../models/restaurant.type';
import { RestaurantItem } from '../restaurant-item/restaurant-item';

@Component({
  selector: 'app-restaurants',
  imports: [RestaurantItem],
  templateUrl: './restaurants.html',
  styleUrl: './restaurants.scss'
})
export class Restaurants implements OnInit {
  private restaurantService = inject(RestaurantService);
  private router = inject(Router);

  restaurants = signal<Array<Restaurant>>([]);

  ngOnInit(): void {
    this.restaurantService.getAllRestaurants()
    .subscribe({
      next: (restaurants) => {
        this.restaurants.set(restaurants);
      },
      error: (err) => {
        this.router.navigate(['/error'], { state: { error: err.error } });
      }
    })
  }
}

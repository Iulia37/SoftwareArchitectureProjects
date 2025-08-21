import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Restaurant } from '../models/restaurant.type'

@Injectable({
  providedIn: 'root'
})
export class RestaurantService {
  http = inject(HttpClient);
  private apiUrl = 'https://localhost:7146/api/Restaurants';

  getAllRestaurants = () => {
    return this.http.get<Array<Restaurant>>(this.apiUrl);
  }

  getRestaurantById = (id: string) => {
    const url = `${this.apiUrl}/${id}`;
    return this.http.get<Restaurant>(url);
  }
}

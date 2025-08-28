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

  getImageUrl(imagePath: string) {
    return 'https://localhost:7146'+ imagePath;
  }

  createRestaurant = (restaurant: Restaurant) => {
    return this.http.post(this.apiUrl, restaurant);
  } 

  updateRestaurant = (restaurant: Restaurant) => {
    const url = `${this.apiUrl}/${restaurant.id}`;
    return this.http.put(url, restaurant);
  }

  deleteRestaurant = (restaurant: Restaurant) => {
    const url = `${this.apiUrl}/${restaurant.id}`;
    return this.http.delete(url);
  }

  uploadImage(formData: FormData) {
    return this.http.post<{ imageUrl: string }>(`${this.apiUrl}/image`, formData);
  }

}

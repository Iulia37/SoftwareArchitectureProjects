import { Component, signal, inject, OnInit, computed, input } from '@angular/core';
import { Restaurant } from '../../models/restaurant.type'
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { RestaurantService } from '../../services/restaurant-service';
import { AuthService } from '../../services/auth-service';
import { MenuItemService } from '../../services/menu-item-service';
import { MenuItem } from '../../models/menu-item.type'
import { forkJoin, of, switchMap } from 'rxjs';
import { CommonModule } from '@angular/common';
import { MenuItem as M } from '../menu-item/menu-item';
import { OrderComponent } from "../order-component/order-component";

@Component({
  selector: 'app-restaurant-details',
  imports: [CommonModule, M, OrderComponent, RouterLink],
  templateUrl: './restaurant-details.html',
  styleUrl: './restaurant-details.scss'
})
export class RestaurantDetails implements OnInit {
  restaurant = signal<Restaurant | null>(null);
  menuItems = signal<Array<MenuItem>>([]);
  imageUrl = computed(() => {
    const r = this.restaurant();
    return r?.imageUrl ? this.restaurantService.getImageUrl(r.imageUrl) : '';
  });

  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private restaurantService = inject(RestaurantService);
  private menuItemsService = inject(MenuItemService);
  authService = inject(AuthService);

  ngOnInit(): void 
  {
    this.route.paramMap.pipe(
      switchMap(params => {
        const id = params.get('id');
        if(id)
        {
          return forkJoin({
            restaurant: this.restaurantService.getRestaurantById(id),
            menuItems: this.menuItemsService.getMenuItemsByProjectId(id)
          });
        }
        return of({ restaurant: null, menuItems: [] });
    })
    ).subscribe({
      next: ({restaurant, menuItems}) => {
        this.restaurant.set(restaurant),
        this.menuItems.set(menuItems)
      },
      error: (err) => {
        this.router.navigate(['/error'], { state: { error: err} });
      }
    });
  }

  deleteRestaurant(restaurant: Restaurant | null){
    if(restaurant != null)
    {
      this.restaurantService.deleteRestaurant(restaurant).subscribe({
        next: () => {
          this.router.navigate(['/restaurants']);
        },
        error: (err) => {
          this.router.navigate(['/error'], { state: { error: err} });
        }
      })
    }
  }

  deleteMenuItem(menuItem: MenuItem){
    this.menuItems.update((items) => {
      const filteredItems = items.filter(it => it.id !== menuItem.id);
      return filteredItems;
    })
  }
}

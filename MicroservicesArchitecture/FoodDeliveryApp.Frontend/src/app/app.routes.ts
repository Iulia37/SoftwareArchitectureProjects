import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path:'',
        pathMatch: 'full',
        loadComponent: () => {
            return import('./components/home/home').then((m) => m.Home);
        }
    },
    {
        path: 'user/login',
        loadComponent: () => {
            return import('./components/user-login/user-login').then((m) => m.UserLogin);
        }
    },
    {
        path: 'user/register',
        loadComponent: () => {
            return import('./components/user-register/user-register').then((m) => m.UserRegister);
        }
    },
    {
        path: 'restaurants',
        loadComponent: () => {
            return import('./components/restaurants/restaurants').then((m) => m.Restaurants);
        }
    },
    {
        path: 'restaurant/create',
        loadComponent: () => {
            return import('./components/restaurant-create/restaurant-create').then((m) => m.RestaurantCreate);
        }
    },
    {
        path: 'restaurant/:id',
        loadComponent: () => {
            return import('./components/restaurant-details/restaurant-details').then((m) => m.RestaurantDetails);
        }
    }
];

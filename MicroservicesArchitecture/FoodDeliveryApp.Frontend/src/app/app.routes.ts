import { Routes } from '@angular/router';
import { authGuard } from './guards/auth-guard';

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
        canActivate: [authGuard],
        data: { roles: ['Admin'] },
        loadComponent: () => {
            return import('./components/restaurant-create/restaurant-create').then((m) => m.RestaurantCreate);
        }
    },
    {
        path: 'restaurant/:id',
        loadComponent: () => {
            return import('./components/restaurant-details/restaurant-details').then((m) => m.RestaurantDetails);
        }
    },
    {
        path: 'restaurant/edit/:id',
        canActivate: [authGuard],
        data: { roles: ['Admin'] },
        loadComponent: () => {
            return import('./components/edit-restaurant/edit-restaurant').then((m) => m.EditRestaurant);
        }
    },
    {
        path: 'menu-item/create/:id',
        canActivate: [authGuard],
        data: { roles: ['Admin'] },
        loadComponent: () => {
            return import('./components/create-menu-item/create-menu-item').then((m) => m.CreateMenuItem);
        }
    },
    {
        path: 'menu-item/edit/:id',
        canActivate: [authGuard],
        data: { roles: ['Admin'] },
        loadComponent: () => {
            return import('./components/edit-menu-item/edit-menu-item').then((m) => m.EditMenuItem);
        }
    },
    {
        path: 'payment',
        canActivate: [authGuard],
        loadComponent: () => {
            return import('./components/payment-component/payment-component').then((m) => m.PaymentComponent);
        }
    },
    {
        path: 'user/:id',
        canActivate: [authGuard],
        loadComponent: () => {
            return import('./components/user-details/user-details').then((m) => m.UserDetails);
        }
    },
    {
        path: 'error',
        loadComponent: () => {
            return import('./components/error/error').then((m) => m.Error);
        }
    }
];

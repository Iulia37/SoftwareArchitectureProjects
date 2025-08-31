import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth-service';
import { CanActivateFn, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

export const authGuard: CanActivateFn = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  if (!authService.isLoggedIn()) {
    router.navigate(['/user/login'], { 
      queryParams: { returnUrl: state.url } 
    });
    return false;
  }

  const allowedRoles: string[] = route.data?.['roles'] || [];
  if (allowedRoles.length > 0) {
    const userRole = authService.user()?.role;
    if (!userRole || !allowedRoles.includes(userRole)) {
      router.navigate(['/']);
      return false;
    }
  }
  
  return true;
};
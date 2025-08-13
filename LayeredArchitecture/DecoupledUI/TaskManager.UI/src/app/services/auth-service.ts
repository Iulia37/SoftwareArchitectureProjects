import { Injectable, inject, signal, computed} from '@angular/core';
import { loginUser } from '../models/login-user.type';
import { User } from '../models/user.type';
import { HttpClient } from '@angular/common/http';
import { tap, catchError } from 'rxjs';
import { jwtDecode } from 'jwt-decode';
import { Router } from '@angular/router';
import { registerUser} from '../models/register-user.type';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private http = inject(HttpClient);
  private apiUrl = 'https://localhost:7119/api/Auth';
  private router = inject(Router);

  private tokenKey = 'auth_token';
  private userKey = 'user_key';
  private _user = signal<User | null>(this.getUserFromStorage());
  private _token = signal<string | null>(this.getTokenFromStorage());
  readonly user = computed(() => this._user());
  readonly isLoggedIn = computed(() => !!this._token());

  registerUser(user: registerUser) {
    const url = `${this.apiUrl}/register`;
    return this.http.post(url, user);
  }

  loginUser(user: loginUser) {
    const url = `${this.apiUrl}/login`;
    return this.http.post(url, user, { responseType: 'text' }).pipe(
      tap((token) => {
        localStorage.setItem(this.tokenKey, token);
        this._token.set(token);

        try{
          const decodedToken: any = jwtDecode(token);
          const userData = {
            id: decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'],
            username: decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'],
            email: decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'],
          } as User;

          localStorage.setItem(this.userKey, JSON.stringify(userData));
          this._user.set(userData);
        }catch (error) {
          this.router.navigate(['/error'], { state: { error: error} });
        }
      }),
      catchError((error) => {
        throw error;
      })
    );
  }

  getToken(): string | null {
    return this._token();
  }
  
  logoutUser(){
    localStorage.removeItem(this.userKey);
    localStorage.removeItem(this.tokenKey);
    this._user.set(null);
    this._token.set(null);
    this.router.navigate(["/"]);
  }

  private getUserFromStorage(): User | null {
    const user = localStorage.getItem(this.userKey);
    return user ? JSON.parse(user) : null;
  }

  private getTokenFromStorage(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

}

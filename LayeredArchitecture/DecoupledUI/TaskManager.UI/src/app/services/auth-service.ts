import { Injectable, inject, signal, computed} from '@angular/core';
import { loginUser } from '../models/login-user.type';
import { User } from '../models/user.type';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private http = inject(HttpClient);
  private apiUrl = 'https://localhost:7119/api/Users';

  private userKey = 'user_key';
  private _user = signal<User | null>(this.getUserFromStorage());
  readonly user = computed(() => this._user());
  readonly isLoggedIn = computed(() => !!this.user());

  loginUser(user: loginUser) {
    const url = `${this.apiUrl}/login`;
    return this.http.post<User>(url, user).pipe(
      tap((user) => {
        localStorage.setItem(this.userKey, JSON.stringify(user));
        this._user.set(user);
      })
    );
  }
  
  logoutUser(){
    localStorage.removeItem(this.userKey);
    this._user.set(null);
  }

  private getUserFromStorage(): User | null {
    const user = localStorage.getItem(this.userKey);
    return user ? JSON.parse(user) : null;
  }
}

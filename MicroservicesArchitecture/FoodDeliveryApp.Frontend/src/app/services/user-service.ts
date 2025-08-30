import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../models/user.type';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private http = inject(HttpClient);
  private apiUrl = 'https://localhost:7176/api/Users';

  getUserById = (id: string) => {
    const url = `${this.apiUrl}/${id}`;
    return this.http.get<User>(url);
  }
}

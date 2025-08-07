import { Injectable, inject } from '@angular/core';
import { User } from '../models/user.type';
import { registerUser} from '../models/register-user.type';
import { HttpClient } from '@angular/common/http'

@Injectable({
  providedIn: 'root'
})
export class UserService {
  http = inject(HttpClient);
  private apiUrl = 'https://localhost:7119/api/Users';
  
  registerUser(user: registerUser) {
    const url = `${this.apiUrl}/register`;
    return this.http.post(url, user);
  }
}

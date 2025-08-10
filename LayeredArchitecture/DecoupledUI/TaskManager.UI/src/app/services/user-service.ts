import { Injectable, inject } from '@angular/core';
import { User } from '../models/user.type';
import { HttpClient } from '@angular/common/http'

@Injectable({
  providedIn: 'root'
})
export class UserService {
  http = inject(HttpClient);
  private apiUrl = 'https://localhost:7119/api/Users';
  
}

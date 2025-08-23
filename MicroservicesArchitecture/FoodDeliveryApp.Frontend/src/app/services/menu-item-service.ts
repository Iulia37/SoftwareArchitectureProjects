import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MenuItem } from '../models/menu-item.type';

@Injectable({
  providedIn: 'root'
})
export class MenuItemService {
  http = inject(HttpClient);
  private apiUrl = 'https://localhost:7146/api/MenuItems';

  getMenuItemsByProjectId = (id: string) => {
    const url = `${this.apiUrl}/${id}`;
    return this.http.get<Array<MenuItem>>(url);
  }
}

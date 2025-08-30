import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MenuItem } from '../models/menu-item.type';

@Injectable({
  providedIn: 'root'
})
export class MenuItemService {
  http = inject(HttpClient);
  private apiUrl = 'https://localhost:7146/api/MenuItems';

  getMenuItemById = (id: string) => {
    const url = `${this.apiUrl}/${id}`;
    return this.http.get<MenuItem>(url);
  }

  getMenuItemsByProjectId = (id: string) => {
    const url = `${this.apiUrl}/restaurant/${id}`;
    return this.http.get<Array<MenuItem>>(url);
  }

  createMenuItem = (menuItem: MenuItem) => {
    return this.http.post<MenuItem>(this.apiUrl, menuItem);
  }

  updateMenuItem = (menuItem: MenuItem) => {
    const url = `${this.apiUrl}/${menuItem.id}`;
    return this.http.put<MenuItem>(url, menuItem);
  }

  deleteMenuItem = (menuItem: MenuItem) => {
    const url = `${this.apiUrl}/${menuItem.id}`;
    return this.http.delete(url);
  }
}

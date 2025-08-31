import { Component, input, inject, output } from '@angular/core';
import { MenuItem as MItem } from '../../models/menu-item.type'
import { MenuItemService } from '../../services/menu-item-service';
import { AuthService } from '../../services/auth-service';
import { Counter } from '../counter/counter';
import { RouterLink, Router } from "@angular/router";

@Component({
  selector: 'app-menu-item',
  imports: [Counter, RouterLink],
  templateUrl: './menu-item.html',
  styleUrl: './menu-item.scss'
})
export class MenuItem {
  private menuItemService = inject(MenuItemService);
  private router = inject(Router);
  authService = inject(AuthService);

  menuItem = input.required<MItem>();
  itemDeleted = output<MItem>();

  deleteMenuItem(menuItem: MItem) {
    this.menuItemService.deleteMenuItem(menuItem).subscribe({
      next: () => {
        this.itemDeleted.emit(this.menuItem());
      },
      error: (err) => {
        this.router.navigate(['/error'], { state: { error: err.error} });
      }
    })
  }
}

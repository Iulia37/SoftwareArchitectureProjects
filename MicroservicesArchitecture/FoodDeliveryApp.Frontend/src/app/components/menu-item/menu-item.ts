import { Component, input, inject, output } from '@angular/core';
import { MenuItem as MItem } from '../../models/menu-item.type'
import { MenuItemService } from '../../services/menu-item-service';
import { Counter } from '../counter/counter';
import { RouterLink } from "@angular/router";

@Component({
  selector: 'app-menu-item',
  imports: [Counter, RouterLink],
  templateUrl: './menu-item.html',
  styleUrl: './menu-item.scss'
})
export class MenuItem {
  private menuItemService = inject(MenuItemService);

  menuItem = input.required<MItem>();
  itemDeleted = output<MItem>();

  deleteMenuItem(menuItem: MItem) {
    this.menuItemService.deleteMenuItem(menuItem).subscribe({
      next: () => {
        this.itemDeleted.emit(this.menuItem());
      },
      error: (err) => {
        
      }
    })
  }
}

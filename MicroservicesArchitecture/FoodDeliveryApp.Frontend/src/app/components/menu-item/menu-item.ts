import { Component, input, inject } from '@angular/core';
import { MenuItem as MItem } from '../../models/menu-item.type'
import { Counter } from '../counter/counter';

@Component({
  selector: 'app-menu-item',
  imports: [Counter],
  templateUrl: './menu-item.html',
  styleUrl: './menu-item.scss'
})
export class MenuItem {
  menuItem = input.required<MItem>();
}

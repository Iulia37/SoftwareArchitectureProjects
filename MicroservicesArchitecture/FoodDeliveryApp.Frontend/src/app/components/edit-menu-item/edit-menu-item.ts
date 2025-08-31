import { Component, inject, OnInit } from '@angular/core';
import { MenuItemService } from '../../services/menu-item-service';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MenuItem } from '../../models/menu-item.type';
import { of, switchMap } from 'rxjs';

@Component({
  selector: 'app-edit-menu-item',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './edit-menu-item.html',
  styleUrl: './edit-menu-item.scss'
})
export class EditMenuItem implements OnInit {
  private menuItemService = inject(MenuItemService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private fb = inject(FormBuilder);

  form!: FormGroup;
  originalItem: MenuItem | null = null;
  errors: string = '';

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
        const id = params.get('id');
        if (id) {
          return this.menuItemService.getMenuItemById(id).subscribe({
            next: (item) => {
              this.originalItem = item;
              this.initForm(item);
            },
            error: (err) => {
              this.router.navigate(['/error'], { state: { error: err.error} });
            }
          });
        }
        this.router.navigate(['/error'], { state: { error: 'Not found' } });
        return;
      }
    )
  }

  initForm(menuItem: MenuItem | null) {
    this.form = this.fb.group({
      name: [menuItem?.name || '', Validators.required],
      description: [menuItem?.description || ''],
      price: [menuItem?.price || ''],
    });
  }

  onSubmit() {
    if(this.form.valid) {
      const formValue = this.form.value;
      const updatedItem: MenuItem = {
        id: Number(this.route.snapshot.paramMap.get('id')!),
        restaurantId: this.originalItem?.restaurantId!,
        name: formValue.name,
        description: formValue.description,
        price: formValue.price      
      };

      this.menuItemService.updateMenuItem(updatedItem).subscribe({
        next: () => {
          this.router.navigate(['/restaurant', this.originalItem?.restaurantId]);
        },
        error: (err) => {
          this.errors = '';
          if(err.error.errors)
          {
            Object.keys(err.error.errors).forEach((field) => {
              this.errors = err.error.errors[field][0]; 
            })
          } else if( typeof err.error == 'string'){
              this.errors = err.error;
          }
        }
      });
    }
  }
}

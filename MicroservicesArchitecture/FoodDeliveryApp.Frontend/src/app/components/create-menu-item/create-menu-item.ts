import { Component, inject, OnInit} from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MenuItem } from '../../models/menu-item.type';
import { MenuItemService } from '../../services/menu-item-service';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule} from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-create-menu-item',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './create-menu-item.html',
  styleUrl: './create-menu-item.scss'
})
export class CreateMenuItem implements OnInit {
  private menuItemService = inject(MenuItemService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private fb = inject(FormBuilder);
  
  form!: FormGroup;
  restaurantId!: string | null;

  errors: string = '';

  ngOnInit(): void {
    this.restaurantId = this.route.snapshot.paramMap.get('id');
    if(this.restaurantId)
    {
      this.form = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      price: [0],
      restaurantId: [Number(this.restaurantId)],
  })
    }
  }

  onSubmit() {
    if(this.form.valid){
      const newMenuItem: MenuItem = this.form.value;
      this.menuItemService.createMenuItem(newMenuItem).subscribe({
        next: () => {
          this.router.navigate(['/restaurant', this.restaurantId]);
        },
        error: (err) => {
          this.errors = '';
          if(err.error.errors) {
            Object.keys(err.error.errors).forEach((field) => {
              this.errors = err.error.errors[field][0];
            })
          } else if(typeof err.error == 'string'){
            this.errors = err.error;
          }
        }
      });
    }
  }
}

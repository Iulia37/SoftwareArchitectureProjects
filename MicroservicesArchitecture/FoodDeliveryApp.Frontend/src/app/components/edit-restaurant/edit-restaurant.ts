import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { of, switchMap } from 'rxjs';
import { Restaurant } from '../../models/restaurant.type';
import { CommonModule } from '@angular/common';
import { RestaurantService } from '../../services/restaurant-service';

@Component({
  selector: 'app-edit-restaurant',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './edit-restaurant.html',
  styleUrl: './edit-restaurant.scss'
})
export class EditRestaurant implements OnInit{
  private restaurantService = inject(RestaurantService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private fb = inject(FormBuilder);

  form!: FormGroup;
  originalRestaurant: Restaurant | null = null;
  error: string = '';
  selectedFile: File | null = null;

  onFileSelected(event: any) {
    const file: File = event.target.files[0];
    if (file) {
      this.selectedFile = file;
    }
  }

  ngOnInit(): void {
    this.route.paramMap.pipe(
      switchMap(params => {
        const id = params.get('id');
        if(id)
        {
          return this.restaurantService.getRestaurantById(id);
        }
        return of(null);
      })
    ).subscribe({
      next: (restaurant) => {
        this.originalRestaurant = restaurant;
        this.initForm(restaurant);
      },
      error: (err) => {
        this.router.navigate(['/error'], { state: { error: err.error} });
      }
    });
  }
  
  initForm(restaurant: Restaurant | null){
    this.form = this.fb.group({
      name: [restaurant?.name, Validators.required],
      email: [restaurant?.email],
      address: [restaurant?.address],
      phoneNumber: [restaurant?.phoneNumber],
      imageUrl: [restaurant?.imageUrl],
    });
  }

  onSubmit() {
      if (this.form.valid) 
      {
        const formValue = this.form.value;
        const updatedRestaurant: Restaurant = {
          id: Number(this.route.snapshot.paramMap.get('id')!),
          name: formValue.name,
          email: formValue.email,
          address: formValue.address,
          phoneNumber: formValue.phoneNumber,
          imageUrl: formValue.imageUrl
        };
      
        if(this.selectedFile) {
          const formData = new FormData();
          formData.append('file', this.selectedFile);

          this.restaurantService.uploadImage(formData).subscribe({
            next: (res: any) => {
              updatedRestaurant.imageUrl = res.imageUrl;

              this.restaurantService.updateRestaurant(updatedRestaurant).subscribe({
                next: () => this.router.navigate(['/restaurant', updatedRestaurant.id]),
                error: (err) => this.handleError(err)
              });
            },
            error: (err) => this.handleError(err)
          });
        } else {
            this.restaurantService.updateRestaurant(updatedRestaurant).subscribe({
              next: () => this.router.navigate(['/restaurant', updatedRestaurant.id]),
              error: (err) => this.handleError(err)
          });
        }
      }
  }

  private handleError(err: any) {
    this.error = '';

    if (err.error.errors) {
      Object.keys(err.error.errors).forEach((field) => {
        this.error = err.error.errors[field][0];
      });
    }
    if(typeof err.error === 'string') {
      this.error = err.error;
    }
  }
}

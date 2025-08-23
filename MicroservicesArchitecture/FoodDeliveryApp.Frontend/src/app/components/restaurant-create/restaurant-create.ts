import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common'
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule} from '@angular/forms';
import { RestaurantService } from '../../services/restaurant-service';
import { Restaurant } from '../../models/restaurant.type';

@Component({
  selector: 'app-restaurant-create',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './restaurant-create.html',
  styleUrl: './restaurant-create.scss'
})
export class RestaurantCreate {
  private router = inject(Router);
  private restaurantService = inject(RestaurantService);
  private fb = inject(FormBuilder);

  error: string = '';
  selectedFile: File | null = null;

  form: FormGroup = this.fb.group({
    name: ['', Validators.required],
    email: ['', Validators.required],
    address: ['', Validators.required],
    phoneNumber: ['', Validators.required]
  })

  onFileSelected(event: any) {
    const file: File = event.target.files[0];
    if (file) {
      this.selectedFile = file;
    }
  }

  onSubmit() {
    if(this.form.valid) {
      const newRestaurant: Restaurant = this.form.value;

    if(this.selectedFile) {
      const formData = new FormData();
      formData.append('file', this.selectedFile);

      this.restaurantService.uploadImage(formData)
        .subscribe({
          next: (res: any) => {
            newRestaurant.imageUrl = res.imageUrl;

            this.restaurantService.createRestaurant(newRestaurant)
              .subscribe({
                next: () => this.router.navigate(['/restaurants']),
                error: (err) => this.handleError(err)
              });
          },
          error: (err) => this.handleError(err)
        });
    } else {
      this.restaurantService.createRestaurant(newRestaurant)
        .subscribe({
          next: () => this.router.navigate(['/restaurants']),
          error: (err) => this.handleError(err)
        });
      }
    }
  }

  private handleError(err: any) {
    console.log(err.error);
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

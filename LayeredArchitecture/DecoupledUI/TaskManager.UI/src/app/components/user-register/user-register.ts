import { Component, inject } from '@angular/core';
import { registerUser } from '../../models/register-user.type';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth-service';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-user-register',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './user-register.html',
  styleUrl: './user-register.scss'
})
export class UserRegister {
  private router = inject(Router);
  private authService = inject(AuthService);
  private fb = inject(FormBuilder);

  errors: string = '';

  form: FormGroup = this.fb.group({
    username: ['', Validators.required],
    password: ['', Validators.required],
    email: ['', [Validators.required]]
  })

  onSubmit() {
    console.log("Sunt in OnSubmit");
    if(this.form.valid)
    {
      console.log("Sunt in OnSubmit if");
      const newUser: registerUser = this.form.value;
      this.authService.registerUser(newUser).subscribe({
        next: () => {
          console.log("Sunt in OnSubmit next");
          this.router.navigate(['/user/login']);
        },
        error: (err) => {
          console.log("Sunt in OnSubmit error");
          this.errors = '';
          
          if(err.error.errors){
            Object.keys(err.error.errors).forEach((field) => {
              this.errors = err.error.errors[field][0];
            })
          } else if(typeof err.error == 'string'){
            this.errors = err.error;
          }
        }
      })
    }
  }
}

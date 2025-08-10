import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth-service';
import { ReactiveFormsModule, FormGroup, FormBuilder, Validators} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { loginUser } from '../../models/login-user.type';

@Component({
  selector: 'app-user-login',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './user-login.html',
  styleUrl: './user-login.scss'
})

export class UserLogin {
  
  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);

  errors: string = '';

  form: FormGroup = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
  });

  onSubmit() {
    if(this.form.valid){

      const user: loginUser = this.form.value;
      
      this.authService.loginUser(user).subscribe({
        next: () => {
          this.router.navigate(["/"]);
        },
        error: (err) => {
          this.errors = '';
          if(err.error.errors){
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

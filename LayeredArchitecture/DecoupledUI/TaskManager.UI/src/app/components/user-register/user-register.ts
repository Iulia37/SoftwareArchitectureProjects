import { Component, inject } from '@angular/core';
import { registerUser } from '../../models/register-user.type';
import { Router } from '@angular/router';
import { UserService } from '../../services/user-service';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';


@Component({
  selector: 'app-user-register',
  imports: [ReactiveFormsModule],
  templateUrl: './user-register.html',
  styleUrl: './user-register.scss'
})
export class UserRegister {
  private router = inject(Router);
  private userService = inject(UserService);
  private fb = inject(FormBuilder);

  form: FormGroup = this.fb.group({
    username: ['', Validators.required],
    password: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]]
  })

  onSubmit() {
    if(this.form.valid)
    {
      const newUser: registerUser = this.form.value;
      this.userService.registerUser(newUser).subscribe({
        next: () => {
          this.router.navigate(['/user/login']);
        },
        error: (err) => {
          console.log(err.error);
        }
      })
    }
  }
}

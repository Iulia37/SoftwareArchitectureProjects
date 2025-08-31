import { Component, inject, OnInit, signal } from '@angular/core';
import { AuthService } from '../../services/auth-service';
import { ActivatedRoute, Router } from '@angular/router';
import { of, switchMap } from 'rxjs';
import { UserService } from '../../services/user-service';
import { User } from '../../models/user.type';

@Component({
  selector: 'app-user-details',
  imports: [],
  templateUrl: './user-details.html',
  styleUrl: './user-details.scss'
})
export class UserDetails implements OnInit {
  private authServide = inject(AuthService);
  private userService = inject(UserService);
  private route = inject(ActivatedRoute); 
  private router = inject(Router);

  user = signal<User | null>(null);

  ngOnInit(): void {
      this.route.paramMap.pipe(
        switchMap(params => {
          const id = params.get('id');
          if(id)
          {
            return this.userService.getUserById(id);
          }
          return of(null);
        })
      ).subscribe({
        next: (user) => {
          this.user.set(user);
        },
        error: (err) => {
          this.router.navigate(['/error'], { state: { error: err.error} });
        }
      });
    }
}

import { Component, signal, inject } from '@angular/core';
import { RouterLink, Router } from '@angular/router';
import { AuthService } from '../../services/auth-service';

@Component({
  selector: 'app-header',
  imports: [RouterLink],
  templateUrl: './header.html',
  styleUrl: './header.scss'
})
export class Header {
  title = signal('TaskManager');
  authService = inject(AuthService);
  private router = inject(Router);

  logoutUser(): void {
    this.authService.logoutUser();
    this.router.navigate(['/']);
  }
}

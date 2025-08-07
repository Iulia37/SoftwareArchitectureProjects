import { Component, signal, inject } from '@angular/core';
import { RouterOutlet, Router } from '@angular/router';
import { Header } from './components/header/header';
import { AuthService } from './services/auth-service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Header],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('TaskManager.UI');
  private authService = inject(AuthService);
  private router = inject(Router);

  constructor() {
    // this.authService.logoutUser();
    // this.router.navigate(["/"]);
  }
}

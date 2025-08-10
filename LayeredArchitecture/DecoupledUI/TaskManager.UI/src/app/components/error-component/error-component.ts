import { Component, signal } from '@angular/core';

@Component({
  selector: 'app-error',
  imports: [],
  templateUrl: './error-component.html',
  styleUrl: './error-component.scss'
})
export class Error {
  error = signal<string>('');

  ngOnInit(): void {
    this.error.set(history.state['error']);
  }
}

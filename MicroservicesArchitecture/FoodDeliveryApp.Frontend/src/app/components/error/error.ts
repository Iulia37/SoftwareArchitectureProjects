import { Component, signal, OnInit } from '@angular/core';

@Component({
  selector: 'app-error',
  imports: [],
  templateUrl: './error.html',
  styleUrl: './error.scss'
})
export class Error implements OnInit{
  error = signal<string>('');

  ngOnInit(): void {
    this.error.set(history.state['error']);
  }
}

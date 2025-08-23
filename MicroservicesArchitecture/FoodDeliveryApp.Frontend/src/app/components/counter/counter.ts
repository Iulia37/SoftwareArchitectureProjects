import { Component, signal } from '@angular/core';

@Component({
  selector: 'app-counter',
  imports: [],
  templateUrl: './counter.html',
  styleUrl: './counter.scss'
})
export class Counter {
  count = signal<number>(0);

  increaseCount()
  {
    this.count.update(c => c + 1);
  }

  decreaseCount()
  {
    this.count.update(c => Math.max(c - 1, 0));
  }
}

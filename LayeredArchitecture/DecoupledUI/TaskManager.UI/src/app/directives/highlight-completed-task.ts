import { Directive, input, effect, inject, ElementRef } from '@angular/core';

@Directive({
  selector: '[appHighlightCompletedTask]',
  standalone: true,
})
export class HighlightCompletedTask {
  isCompleted = input(false);
  el = inject(ElementRef);
  stylesEffect = effect(() => {
    if (this.isCompleted()) {
      this.el.nativeElement.style.textDecoration = 'line-through';
      this.el.nativeElement.style.color = '#6c757d';
    } else {
      this.el.nativeElement.style.textDecoration = 'none';
    }
  });
}
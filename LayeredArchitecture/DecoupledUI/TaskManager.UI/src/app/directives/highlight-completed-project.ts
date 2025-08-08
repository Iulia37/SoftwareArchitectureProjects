import { Directive, ElementRef, input, inject, effect } from '@angular/core';

@Directive({
  selector: '[appHighlightCompletedProject]',
  standalone: true,
})
export class HighlightCompletedProject {
  isCompleted = input(false);
  ef = inject(ElementRef);

  stylesEffect = effect(() => {
    if(this.isCompleted())
    {
      this.ef.nativeElement.style.color = 'grey';
      this.ef.nativeElement.style.filter = 'grayscale(0.7)';
      const badge = document.createElement('span');
      badge.textContent = '✔ Completed';
      badge.classList.add('completed-badge');
      this.ef.nativeElement.prepend(badge)
    }
  })
}

import { Component, input } from '@angular/core';
import { Project } from '../../models/project.type';
import { DatePipe, CommonModule } from '@angular/common';
import { TruncatePipe } from '../../pipes/truncate-pipe';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-project-item',
  imports: [DatePipe, TruncatePipe, RouterLink, CommonModule],
  templateUrl: './project-item.html',
  styleUrl: './project-item.scss'
})
export class ProjectItem {
  project = input.required<Project>();
}

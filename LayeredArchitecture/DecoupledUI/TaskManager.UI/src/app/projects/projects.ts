import { Component, inject, OnInit, signal } from '@angular/core';
import { ProjectService } from '../services/project-service';
import { AuthService } from '../services/auth-service';
import { Project } from '../models/project.type';
import { catchError } from 'rxjs';
import { ProjectItem } from '../components/project-item/project-item';

@Component({
  selector: 'app-projects',
  imports: [ProjectItem],
  templateUrl: './projects.html',
  styleUrl: './projects.scss'
})
export class Projects implements OnInit {
  projectService = inject(ProjectService);
  authService = inject(AuthService);
  projectItems = signal<Array<Project>>([]);

  ngOnInit(): void {
    const userId = this.authService.user()?.id;
    if(userId)
    {
      this.projectService
      .getProjectsByUserId(userId.toString())
      .pipe(
        catchError((err) => {
          console.log(err);
          throw err;
        })
      )
      .subscribe((projects) => {
        this.projectItems.set(projects);
      });
    }
  }

  deleteProjectItem(projectItem: Project){
    this.projectItems.update((projects) => {
      const filteredProjects = projects.filter(project => project.id != projectItem.id);
      return filteredProjects;
    })
  }
}

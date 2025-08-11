import { Component, inject, OnInit, signal } from '@angular/core';
import { ProjectService } from '../services/project-service';
import { AuthService } from '../services/auth-service';
import { Project } from '../models/project.type';
import { catchError } from 'rxjs';
import { Router } from '@angular/router';
import { ProjectItem } from '../components/project-item/project-item';

@Component({
  selector: 'app-projects',
  imports: [ProjectItem],
  templateUrl: './projects.html',
  styleUrl: './projects.scss'
})
export class Projects implements OnInit {
  private projectService = inject(ProjectService);
  private authService = inject(AuthService);
  private router = inject(Router)
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
        if(this.projectItems().length == 0){
          this.router.navigate(['/']);
        }
      });
    }
  }

}

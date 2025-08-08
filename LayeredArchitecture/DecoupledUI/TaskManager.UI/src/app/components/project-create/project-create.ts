import { Component, inject, OnInit } from '@angular/core';
import { Project } from '../../models/project.type';
import { ProjectService } from '../../services/project-service';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule} from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth-service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-project-create',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './project-create.html',
  styleUrl: './project-create.scss'
})
export class ProjectCreate {
  private projectService = inject(ProjectService);
  private fb = inject(FormBuilder);
  private route = inject(Router);
  private authService = inject(AuthService);

  error: string = '';

  form: FormGroup = this.fb.group({
    name: ['', Validators.required],
    description: ['', Validators.required],
    isCompleted: [false],
    deadline: ['', Validators.required],
    createdDate: [new Date()],
    userId: [this.authService.user()?.id]
  });

  onSubmit() {
  if(this.form.valid) {
      const newProject: Project = this.form.value;
      this.projectService.createProject(newProject).subscribe({
        next: () => {
          this.route.navigate(['/projects']);
        },
        error: (err) => {
          console.log(err.error);

          this.error = '';

          if (err.error.errors) {
            Object.keys(err.error.errors).forEach((field) => {
              this.error = err.error.errors[field][0];
            });
          }
          if (typeof err.error === 'string') {
            this.error = err.error;
          }
        }
      });
    }
  }
}

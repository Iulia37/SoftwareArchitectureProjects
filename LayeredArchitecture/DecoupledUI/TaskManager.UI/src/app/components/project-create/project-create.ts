import { Component, inject, OnInit } from '@angular/core';
import { Project } from '../../models/project.type';
import { ProjectService } from '../../services/project-service';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule} from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-project-create',
  imports: [ReactiveFormsModule],
  templateUrl: './project-create.html',
  styleUrl: './project-create.scss'
})
export class ProjectCreate {
  private projectService = inject(ProjectService);
  private fb = inject(FormBuilder);
  private route = inject(Router);

  form: FormGroup = this.fb.group({
    name: ['', Validators.required],
    description: ['', Validators.required],
    isCompleted: [false],
    deadline: ['', Validators.required],
    createdDate: [new Date()],
    userId: [22]
  });

  onSubmit() {
  if(this.form.valid) {
      const newProject: Project = this.form.value;
      this.projectService.createProject(newProject).subscribe(() => {
        this.route.navigate(['/projects']);
      });
    }
  }
}

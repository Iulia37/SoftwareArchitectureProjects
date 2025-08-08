import { Component, inject, OnInit } from '@angular/core';
import { Project } from '../../models/project.type';
import { ProjectService } from '../../services/project-service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { of, switchMap } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-project-edit',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './project-edit.html',
  styleUrl: './project-edit.scss'
})
export class ProjectEdit implements OnInit{
  private route = inject(ActivatedRoute);
  private projectService = inject(ProjectService);
  private router = inject(Router);
  private fb = inject(FormBuilder);

  form!: FormGroup;
  originalProject: Project | null = null;
  errors: string = '';

  ngOnInit(): void {
    this.route.paramMap.pipe(
      switchMap(params => {
        const id = params.get('id');
        if(id)
        {
          return this.projectService.getProjectById(id);
        }
        return of(null);
      })
    ).subscribe((project) => {
      this.originalProject = project;
      this.initForm(project);
    })
  }

  initForm(project: Project | null){
    this.form = this.fb.group({
      name: [project?.name || '', Validators.required],
      description: [project?.description || ''],
      deadline: [project?.deadline ? this.formatDate(project.deadline) : ''],
    });
  }

  formatDate(date: Date): string {
    return new Date(date).toISOString().split('T')[0];
  }

  onSubmit() {
      if (this.form.valid) {
        const formValue = this.form.value;
        const updatedProject: Project = {
          id: Number(this.route.snapshot.paramMap.get('id')!),
          userId: this.originalProject?.userId!,
          name: formValue.name,
          description: formValue.description,
          deadline: new Date(formValue.deadline),
          isCompleted: this.originalProject?.isCompleted || false,
          createdDate: this.originalProject?.createdDate || new Date(),
        };
  
        this.projectService.updateProject(updatedProject).subscribe({
          next: () => {
            this.router.navigate(['/project', this.originalProject?.id]);
          },
          error: (err) => {
            this.errors = '';
            if(err.error.errors)
            {
              Object.keys(err.error.errors).forEach((field) => {
                this.errors = err.error.errors[field][0];
              })
            }
            else if(typeof err.error == 'string'){
              this.errors = err.error;
            }
          }
        });
      }
    }
}

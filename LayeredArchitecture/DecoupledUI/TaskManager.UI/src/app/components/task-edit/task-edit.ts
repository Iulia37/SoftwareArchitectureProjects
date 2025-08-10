import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TaskService } from '../../services/task-service'; 
import { Task } from '../../models/task.type';
import { switchMap } from 'rxjs/operators';
import { of } from 'rxjs';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-task-edit',
  templateUrl: './task-edit.html',
  styleUrls: ['./task-edit.scss'],
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
})
export class TaskEdit implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private taskService = inject(TaskService);
  private fb = inject(FormBuilder);

  form!: FormGroup;
  originalTask: Task | null = null;

  errors: string = '';

  ngOnInit() {
    this.route.paramMap.pipe(
      switchMap(params => {
        const id = params.get('id');
        if (id) {
          return this.taskService.getTaskById(id);
        }
        return of(null);
      })
    ).subscribe({
      next: (task) => {
        this.originalTask = task;
        this.initForm(task);
      },
      error: (err) => {
        this.router.navigate(['/error'], { state: { error: err} });
      }
    });
  }

  initForm(task: Task | null) {
    this.form = this.fb.group({
      title: [task?.title || '', Validators.required],
      description: [task?.description || ''],
      deadline: [task?.deadline ? this.formatDate(task.deadline) : ''],
    });
  }

  formatDate(date: Date): string {
    return new Date(date).toISOString().split('T')[0];
  }

  onSubmit() {
    if (this.form.valid) {
      const formValue = this.form.value;
      const updatedTask: Task = {
        id: Number(this.route.snapshot.paramMap.get('id')!),
        projectId: this.originalTask?.projectId!,
        title: formValue.title,
        description: formValue.description,
        deadline: new Date(formValue.deadline),
        isCompleted: this.originalTask?.isCompleted || false,
        createdDate: this.originalTask?.createdDate || new Date(),
      };

      this.taskService.updateTask(updatedTask).subscribe({
        next: () => {
          this.router.navigate(['/project', this.originalTask?.projectId]);
        },
        error: (err) => {
          this.errors = '';
          if(err.error.errors)
          {
            Object.keys(err.error.errors).forEach((field) => {
              this.errors = err.error.errors[field][0]; 
            })
          } else if( typeof err.error == 'string'){
              this.errors = err.error;
          }
        }
      });
    }
  }
}

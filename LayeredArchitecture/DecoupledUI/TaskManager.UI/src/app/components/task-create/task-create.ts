import { Component, inject, OnInit} from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Task } from '../../models/task.type';
import { TaskService } from '../../services/task-service';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule} from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-task-create',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './task-create.html',
  styleUrl: './task-create.scss'
})
export class TaskCreate implements OnInit {
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private taskService = inject(TaskService);
  private fb = inject(FormBuilder);
  
  form!: FormGroup;
  projectId!: string | null;

  errors: string = '';

  ngOnInit(): void {
    this.projectId = this.route.snapshot.paramMap.get('id');
    if(this.projectId)
    {
      this.form = this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      isCompleted: [false],
      deadline: ['', Validators.required],
      createdDate: [new Date()],
      projectId: [Number(this.projectId)],
  })
    }
  }

  onSubmit() {
    if(this.form.valid){
      const newTask: Task = this.form.value;
      this.taskService.createTask(newTask).subscribe({
        next: (response) => {
          console.log(response);
          this.router.navigate(['/project', this.projectId]);
        },
        error: (err) => {
          this.errors = '';
          if(err.error.errors) {
            Object.keys(err.error.errors).forEach((field) => {
              this.errors = err.error.errors[field][0];
            })
          } else if(typeof err.error == 'string'){
            this.errors = err.error;
          }
        }
      });
    }
  }
}

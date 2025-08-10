import { Injectable, inject } from '@angular/core';
import { Task } from '../models/task.type';
import { HttpClient } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  http = inject(HttpClient);
  private apiUrl = 'https://localhost:7119/api/Tasks';

  getTasksByProjectId = (id: string) => {
    const url = `https://localhost:7119/api/Projects/${id}/tasks`;
    return this.http.get<Array<Task>>(url);
  }

  getTaskById = (id: string) =>
  {
    const url = `${this.apiUrl}/${id}`;
    return this.http.get<Task>(url).pipe(
      catchError((error) => {
        console.log(error);
        return throwError(() => error.error);
      })
    );
  }

  markTaskCompleted = (task: Task) => {
    const url = `${this.apiUrl}/${task.id}/complete`;
    return this.http.post(url, task);
  }

  updateTask = (editedTask: Task) => {
    const url = `${this.apiUrl}/${editedTask.id}`;
    return this.http.put(url, editedTask);
  }

  createTask = (task: Task) => {
    return this.http.post(this.apiUrl, task);
  }

  deleteTask = (task: Task) => {
    const url = `${this.apiUrl}/${task.id}`;
    return this.http.delete(url);
  }
}

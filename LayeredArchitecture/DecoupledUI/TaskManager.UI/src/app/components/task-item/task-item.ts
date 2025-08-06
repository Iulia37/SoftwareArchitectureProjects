import { Component, input, output, inject } from '@angular/core';
import { Task } from '../../models/task.type';
import { FormsModule } from '@angular/forms';
import { HighlightCompletedTask } from '../../directives/highlight-completed-task';
import { RouterLink } from '@angular/router';
import { TaskService } from '../../services/task-service';

@Component({
  selector: 'app-task-item',
  imports: [FormsModule, HighlightCompletedTask, RouterLink],
  templateUrl: './task-item.html',
  styleUrl: './task-item.scss'
})
export class TaskItem {
  task = input.required<Task>();
  taskToggled = output<Task>();
  taskDeleted = output<Task>();
  taskService = inject(TaskService);

  taskChecked() { 
    
    if (this.task().isCompleted) {
      return;
    }
    this.taskToggled.emit(this.task());
  }

  deleteTask(task: Task) {
    this.taskService.deleteTask(task).subscribe({
        next: (response) => {
          console.log(response);
          this.taskDeleted.emit(task);
        },
        error: (err) => {
          console.error(err);
        }
      });
  }
}

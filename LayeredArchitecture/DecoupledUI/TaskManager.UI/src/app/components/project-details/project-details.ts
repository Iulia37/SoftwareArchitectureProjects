import { Component, inject, signal, output } from '@angular/core';
import { Project } from '../../models/project.type';
import { Task } from '../../models/task.type';
import { DatePipe } from '@angular/common';
import { ProjectService } from '../../services/project-service';
import { TaskService } from '../../services/task-service';
import { switchMap, forkJoin, of } from 'rxjs';
import { TaskItem } from '../task-item/task-item';
import { RouterLink, Router, ActivatedRoute } from '@angular/router';
import { HighlightCompletedProject } from '../../directives/highlight-completed-project';

@Component({
  selector: 'app-project-details',
  imports: [DatePipe, TaskItem, RouterLink, HighlightCompletedProject],
  templateUrl: './project-details.html',
  styleUrl: './project-details.scss'
})
export class ProjectDetails {
  private route = inject(ActivatedRoute);
  private projectService = inject(ProjectService);
  private taskService = inject(TaskService);
  private router = inject(Router);

  project = signal<Project | null>(null);
  taskItems = signal<Array<Task>>([]);

  ngOnInit(): void {
  this.route.paramMap.pipe(
    switchMap(params => {
      const id = params.get('id');
      if (id) {
        return forkJoin({
          project: this.projectService.getProjectById(id),
          tasks: this.taskService.getTasksByProjectId(id)
        });
      }
      return of({ project: null, tasks: [] });
    })
    ).subscribe(({ project, tasks }) => {
      this.project.set(project);
      this.taskItems.set(tasks);
    });
  }

  completeTaskItem(taskItem: Task) {
    let completedTask: Task | null = null;

    this.taskItems.update((tasks) => {
      return tasks.map((task) => {
        if (task.id === taskItem.id) {
          completedTask = {
            ...task,
            isCompleted: true,
          };
          return completedTask;
        }
        return task;
      });
    });

    if (completedTask) {
      this.taskService.markTaskCompleted(completedTask).subscribe({
        error: (err) => {
          console.error(err);
        }
      });
    }
  }

  deleteTaskItem(taskItem: Task) {
    this.taskItems.update((tasks) => {
      const filteredTasks = tasks.filter(task => task.id !== taskItem.id);
      return filteredTasks;
    });
  }

  deleteProject(project: Project) {
    this.projectService.deleteProject(project).subscribe({
      next: () =>{
        this.router.navigate(['/projects']);
      },
      error: (err) =>{
        console.log(err);
      }
    })
  }

  completeProject(project: Project) {
    this.projectService.completeProject(project).subscribe({
      next: () => {
        this.router.navigate(['/project', project.id]);
      },
      error: (err) => {
        console.error(err.error);
      }
    });
  }
}

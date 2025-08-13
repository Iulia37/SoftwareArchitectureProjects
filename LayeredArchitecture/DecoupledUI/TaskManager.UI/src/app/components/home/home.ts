import { Component, inject, signal, computed, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth-service';
import { ProjectService } from '../../services/project-service';
import { TaskService } from '../../services/task-service';
import { Project } from '../../models/project.type';
import { Task } from '../../models/task.type';
import { CommonModule } from '@angular/common';
import { RouterLink, Router } from '@angular/router';
import { forkJoin, of, switchMap } from 'rxjs';

@Component({
  selector: 'app-home',
  imports: [CommonModule, RouterLink],
  templateUrl: './home.html',
  styleUrl: './home.scss'
})
export class Home implements OnInit {
  private authService = inject(AuthService);
  private projectService = inject(ProjectService);
  private taskService = inject(TaskService);
  private router = inject(Router);

  projects = signal<Project[]>([]);
  allTasks = signal<Task[]>([]);

  welcomeMessage = computed(() => {
    const user = this.authService.user();
    const hour = new Date().getHours();
    const greeting = hour < 12 ? 'Good morning' : hour < 18 ? 'Good afternoon' : 'Good evening';
    return `${greeting}, ${user?.username}!`;
  });

  totalProjects = computed(() => this.projects().length);
  
  activeProjects = computed(() => 
    this.projects().filter(p => !p.isCompleted).length
  );

  completedProjects = computed(() => 
    this.projects().filter(p => p.isCompleted).length
  );

  totalTasks = computed(() => this.allTasks().length);

  completedTasks = computed(() => 
    this.allTasks().filter(t => t.isCompleted).length
  );

  pendingTasks = computed(() => 
    this.allTasks().filter(t => !t.isCompleted).length
  );

  upcomingDeadlines = computed(() => {
    const today = new Date();
    const nextWeek = new Date(today.getTime() + 7 * 24 * 60 * 60 * 1000);
    
    return this.projects().filter(p => {
      if (!p.deadline || p.isCompleted) return false;
      const deadline = new Date(p.deadline);
      return deadline >= today && deadline <= nextWeek;
    }).length;
  });

  ngOnInit() {
    const userId = this.authService.user()?.id;
    if (userId) {
      this.loadDashboardData(userId.toString());
    }
  }

  private loadDashboardData(userId: string) {
    this.projectService.getProjectsByUserId(userId).pipe(
      switchMap(projects => {
        this.projects.set(projects);
        
        if (projects.length === 0) {
          return of([]);
        }

        const taskRequests = projects.map(project => 
          this.taskService.getTasksByProjectId(project.id.toString())
        );
        
        return forkJoin(taskRequests);
      })
    ).subscribe({
      next: (allTaskArrays) => {
        const flattenedTasks = allTaskArrays.flat();
        this.allTasks.set(flattenedTasks);
      },
      error: (err) => {
        this.router.navigate(['/error'], { state: { error: err} });
      }
    });
  }

  isLoggedIn() {
    return this.authService.isLoggedIn();
  }
}

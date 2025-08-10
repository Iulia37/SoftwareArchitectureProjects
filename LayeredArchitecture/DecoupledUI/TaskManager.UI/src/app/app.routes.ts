import { Routes } from '@angular/router';
import { authGuard } from './guards/auth.guard';

export const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    loadComponent: () => {
      return import('./components/home/home').then((m) => m.Home);
    },
  },
  {
    path: 'projects',
    canActivate: [authGuard],
    loadComponent: () => {
      return import('./projects/projects').then((m) => m.Projects);
    }
  },
  {
    path: 'project/create',
    canActivate: [authGuard],
    loadComponent: () => {
      return import('./components/project-create/project-create').then((m) => m.ProjectCreate);
    }
  },
  {
    path: 'project/:id',
    canActivate: [authGuard],
    loadComponent: () => {
      return import('./components/project-details/project-details').then((m) => m.ProjectDetails);
    }
  },
  {
    path: 'project/:id/edit',
    canActivate: [authGuard],
    loadComponent: () => {
      return import('./components/project-edit/project-edit').then((m) => m.ProjectEdit);
    }
  },
  {
    path: 'task/create/:id',
    canActivate: [authGuard],
    loadComponent: () => {
      return import('./components/task-create/task-create').then((m) => m.TaskCreate);
    }
  },
  {
    path: 'task/:id/edit',
    canActivate: [authGuard],
    loadComponent: () => {
      return import('./components/task-edit/task-edit').then((m) => m.TaskEdit);
    }
  },
  {
    path: 'user/login',
    loadComponent: () => {
      return import('./components/user-login/user-login').then((m) => m.UserLogin);
    }
  },
  {
    path: 'user/register',
    loadComponent: () => {
      return import('./components/user-register/user-register').then((m) => m.UserRegister);
    }
  },
  {
    path: 'error',
    loadComponent: () => {
      return import('./components/error-component/error-component').then((m) => m.Error);
    }
  }
];
import { Routes } from '@angular/router';

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
    loadComponent: () => {
      return import('./projects/projects').then((m) => m.Projects);
    }
  },
  {
    path: 'project/create',
    loadComponent: () => {
      return import('./components/project-create/project-create').then((m) => m.ProjectCreate);
    }
  },
  {
    path: 'project/:id',
    loadComponent: () => {
      return import('./components/project-details/project-details').then((m) => m.ProjectDetails);
    }
  },
  {
    path: 'project/:id/edit',
    loadComponent: () => {
      return import('./components/project-edit/project-edit').then((m) => m.ProjectEdit);
    }
  },
  {
    path: 'task/create/:id',
    loadComponent: () => {
      return import('./components/task-create/task-create').then((m) => m.TaskCreate);
    }
  },
  {
    path: 'task/:id/edit',
    loadComponent: () => {
      return import('./components/task-edit/task-edit').then((m) => m.TaskEdit);
    }
  },
];
import { Injectable, inject } from '@angular/core';
import { Project } from '../models/project.type';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {
  http = inject(HttpClient);
  private apiUrl = 'https://localhost:7119/api/Projects';

  getProjectsByUserId = (id: string) => {
    const url = `${this.apiUrl}/user/${id}`;
    return this.http.get<Array<Project>>(url);
  }

  getProjectById = (id: string) => {
    const url = `${this.apiUrl}/${id}`;
    return this.http.get<Project>(url);
  }

  updateProject = (project: Project) => {
    const url = `${this.apiUrl}/${project.id}`;
    return this.http.put(url, project);
  }

  createProject = (project: Project) => {
    return this.http.post(this.apiUrl, project);
  }

  deleteProject = (project: Project) => {
    const url = `${this.apiUrl}/${project.id}`;
    return this.http.delete(url);
  }
}

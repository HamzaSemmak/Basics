import { Injectable } from '@angular/core';
import { Task }  from '../Task';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
  })
}

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private apiURL: string = 'http://localhost:5000/tasks'

  constructor(private http: HttpClient) { }

  getTasks(): Observable<Task[]> {
    return this.http.get<Task[]>(this.apiURL);
  }

  deleteTask(task: Task): Observable<Task> {
    const Url = `${this.apiURL}/${task.id}`;
    return this.http.delete<Task>(Url); 
  }

  editTask(task: Task): Observable<Task> {
    const Url = `${this.apiURL}/${task.id}`;
    return this.http.put<Task>(Url, task, httpOptions); 
  }

  addTask(task: Task): Observable<Task> {
    return this.http.post<Task>(this.apiURL, task, httpOptions);
  }
}

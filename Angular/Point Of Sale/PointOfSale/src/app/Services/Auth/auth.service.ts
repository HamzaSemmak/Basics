import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from 'src/app/Modules/Users';
import { ApiUsers } from 'src/app/Modules/Global';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
  })
}

@Injectable({
  providedIn: 'root'
})

export class AuthService {

  constructor(private HttpClient: HttpClient) { }

  login(name: any, password: any): Observable<User> {
    return this.HttpClient.get<User>(`${ApiUsers}?email=${name}&password=${password}`, httpOptions);
  }
  

}

/*
  Find(name: any, password: any) {
    return this.Http.get(this.Url + '?name=' + name + '&password=' + password);
  }

    getTasks(): Observable<Task[]> {
    return this.http.get<Task[]>(this.apiURL);
  }
*/
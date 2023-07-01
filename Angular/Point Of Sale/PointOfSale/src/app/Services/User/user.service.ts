import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiUsers } from 'src/app/Modules/Config/Api';
import { User } from 'src/app/Modules/Model/Users';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
  })
}

@Injectable({
  providedIn: 'root'
})

export class UserService {

  constructor(private HttpClient: HttpClient) { }

  all(): Observable<User[]> {
    return this.HttpClient.get<User[]>(ApiUsers, httpOptions);
  }

}

import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from 'src/app/Modules/Model/Users';
import { ApiUsers } from 'src/app/Modules/Config/Api';
import { Keys } from 'src/app/Modules/Config/Config';
import { Router } from '@angular/router';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
  })
}

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  constructor(private HttpClient: HttpClient, private router: Router) { }

  login(name: any, password: any): Observable<User> {
    return this.HttpClient.get<User>(`${ApiUsers}?email=${name}&password=${password}`, httpOptions);
  }

  guard(email: any): void {
    let Key: any;
    this.HttpClient.get(`${ApiUsers}?email=${email}`).subscribe( res => {
      let response = Object.entries(res);
      Key = response[0][1].Key;
      sessionStorage.setItem(Keys, Key);
      this.router.navigate(["/"]);
    })
  }

  User(): Observable<User> {
    var Key = sessionStorage.getItem(Keys);
    return this.HttpClient.get<User>(`${ApiUsers}?Key=${Key}`, httpOptions);
  }
  
  Check(): boolean {
    if(sessionStorage.getItem(Keys))
    {
      return true;
    }
    else {
      return false;
    }
  }

  forgotPassword(email: any): Observable<User> {
    return this.HttpClient.get<User>(`${ApiUsers}?email=${email}`, httpOptions);
  }
}
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from 'src/app/Modules/Model/Users';
import { ApiUsers } from 'src/app/Modules/Config/Api';
import { Keys } from 'src/app/Modules/Config/Config';

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

  guard(email: any): void {
    let Key: any;
    this.HttpClient.get(`${ApiUsers}?email=${email}`).subscribe( res => {
      let response = Object.entries(res);
      Key = response[0][1].Key;
      sessionStorage.setItem(Keys, Key);
    })
  }
  
  Check(): boolean {
    var Key = sessionStorage.getItem(Keys);
    var response: boolean = false;
    this.HttpClient.get<User>(`${ApiUsers}?Key=${Key}`, httpOptions).subscribe( res => {
      if(Object.keys(res).length < 0)
      {
        response = true;
        console.log("1" + response);
      } 
      console.log("2" + response);
    });
    console.log("3" + response);
    return response;
    // Hamza Semmak
  }

}
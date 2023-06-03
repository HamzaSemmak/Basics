import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  Url: string = 'http://localhost:3000/users';

  constructor(private Http: HttpClient) { }

  All() {
    return this.Http.get(this.Url);
  }

  Find(name: any, password: any) {
    return this.Http.get(this.Url + '?name=' + name + '&password=' + password);
  }

  Create(User: any) {
    return this.Http.post(this.Url, User);
  }

  Update(ID: any, User: any) {
    return this.Http.put(this.Update + '/' + ID, User);
  }

  Check(): Boolean {
    return sessionStorage.getItem("User")!=null;
  }

  User() {
    return this.Http.get(`${this.Url}/${sessionStorage.getItem("User")}`);
  }

  LoggOut() {
    sessionStorage.clear();
  }
}

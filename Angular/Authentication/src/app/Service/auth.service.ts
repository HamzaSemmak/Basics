import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  Url: string = 'http://localhost:3000/users';
  Role: string = 'http://localhost:3000/roles';

  constructor(private Http: HttpClient) { }

  All() {
    return this.Http.get(this.Url);
  }

  Find(name: any, password: any) {
    return this.Http.get(this.Url + '?name=' + name + '&password=' + password);
  }

  FindByID(id: any) {
    return this.Http.get(this.Url + '/' + id);
  }

  Create(User: any) {
    return this.Http.post(this.Url, User);
  }

  Update(ID: any, User: any) {
    return this.Http.put(this.Url + '/' + ID, User);
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

  Roles() {
    return this.Http.get(this.Role);
  }
}

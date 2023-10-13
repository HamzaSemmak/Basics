import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { AuthService } from 'src/app/Services/Auth/auth.service';
import { Observable } from 'rxjs';
import { ToastService } from 'src/app/Services/Toast/toast.service';
import { User } from 'src/app/Modules/Model/Users';

@Injectable({
  providedIn: 'root'
})

export class AdminGuard implements CanActivate {
  User: User;

  constructor(private service: AuthService, private router: Router, private Toast: ToastService) {}

  canActivate(
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    if(sessionStorage.getItem('Role') == "admin") {
      return true;
    }
    else {
      this.Toast.warning("You d'ont have acess to this page.")
      return false;
    }
  }
}
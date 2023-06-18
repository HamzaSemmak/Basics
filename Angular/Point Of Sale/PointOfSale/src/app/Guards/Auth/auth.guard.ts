import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { AuthService } from 'src/app/Services/Auth/auth.service';
import { Observable } from 'rxjs';
import { User } from 'src/app/Modules/Model/Users';
import { ToastService } from 'src/app/Services/Toast/toast.service';

@Injectable({
  providedIn: 'root'
})

export class AuthGuard implements CanActivate {

  constructor(private service: AuthService, private router: Router, private Toast: ToastService) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      if(this.service.Check())
      {
        return true;
      }
      else {
        this.Toast.warning("You have to login first.")
        this.router.navigate(['/auth/login']);
        return false;
      }
    }
}
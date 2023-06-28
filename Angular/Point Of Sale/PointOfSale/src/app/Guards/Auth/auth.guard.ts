import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { AuthService } from 'src/app/Services/Auth/auth.service';
import { Observable } from 'rxjs';
import { ToastService } from 'src/app/Services/Toast/toast.service';

@Injectable({
  providedIn: 'root'
})

export class AuthGuard implements CanActivate {

  constructor(private service: AuthService, private router: Router, private Toast: ToastService) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      if(this.service.Check())
      {
        return true;
      }
      else {
        this.router.navigate(['/auth/login']);
        this.Toast.warning('You have to log first');
        return false;
      }
    }
}
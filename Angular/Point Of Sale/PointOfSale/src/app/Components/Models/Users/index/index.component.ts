import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Keys } from 'src/app/Modules/Config/Config';
import { User } from 'src/app/Modules/Model/Users';
import { AuthService } from 'src/app/Services/Auth/auth.service';
import { ToastService } from 'src/app/Services/Toast/toast.service';
import { UserService } from 'src/app/Services/User/user.service';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})

export class IndexComponent implements OnInit {
  Users: User[];
  User: User;

  constructor(
    private userService: UserService, 
    private AuthService: AuthService,
    private toast: ToastService,
    private router: Router
  ) 
  {}

  ngOnInit(): void {
    this.userService.all().subscribe(
      (response) => {
        this.Users = response;
      }
    );
  }

  ngDeleteUser(item: User): void {
    this.userService.delete(item).subscribe(() => {
      this.Users = this.Users.filter(u => u.id != item.id);
      if(item.Key == sessionStorage.getItem(Keys)) {
        this.router.navigate(['/auth/logout/account']);
        return;
      }
    });
    this.toast.success('Record has been successfully deleted') 
  }
}

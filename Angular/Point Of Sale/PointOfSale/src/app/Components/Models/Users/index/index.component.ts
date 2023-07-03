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
  Modal: HTMLElement | null

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

    this.User = {
      id: 1,
      name: "Hamza Semmak",
      gender: "Male",
      email: "hamza@gmail.com",
      password: "AA102374h",
      Key: "9f59b8e8-f750-4db3-aaea-6882a4ad8188",
      role: "admin"
    }
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

  ngShowUser(item: User): void {
    this.Modal = document.querySelector('.Modal')
    this.Modal?.classList.add('showModal');
    this.User = item;
  }

  ngUpdateUser(item: User): void {
    this.router.navigate([`users/update/${item.Key}/${Object.values(item).toString()}`]);
  }
}

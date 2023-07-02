import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/Modules/Model/Users';
import { ToastService } from 'src/app/Services/Toast/toast.service';
import { UserService } from 'src/app/Services/User/user.service';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})

export class IndexComponent implements OnInit {
  Users: User[];

  constructor(private userService: UserService, private toast: ToastService) {}

  ngOnInit(): void {
    this.userService.all().subscribe(
      (response) => {
        this.Users = response;
      }
    );
  }

  ngDeleteUser(item: User): void {
    this.userService.delete(item).subscribe(() => {
      this.Users = this.Users.filter(u => u.id != item.id)
    });
    this.toast.success('Record has been successfully deleted') 
  }
}

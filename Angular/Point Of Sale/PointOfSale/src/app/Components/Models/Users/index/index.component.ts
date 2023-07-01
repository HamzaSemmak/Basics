import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/Modules/Model/Users';
import { UserService } from 'src/app/Services/User/user.service';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})

export class IndexComponent implements OnInit {
  Users: User[];

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.userService.all().subscribe(
      (response) => {
        this.Users = response;
      }
    );
  }
}

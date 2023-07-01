import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/Modules/Model/Users';
import { AuthService } from 'src/app/Services/Auth/auth.service';

@Component({
  selector: 'navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'] 
})

export class NavbarComponent implements OnInit {
  User: User;
  Name: string;
  Bool: boolean;

  constructor(private AuthService: AuthService) { }

  ngOnInit(): void {
    this.AuthService.User().subscribe(
      response => {
        let res = Object.entries(response);
        this.User = { 
          id: res[0][1].id,
          name:  res[0][1].name,
          gender:  res[0][1].gender,
          email:  res[0][1].email,
          password:  res[0][1].password,
          Key:  res[0][1].Key,
          role:  res[0][1].role,
        }
        this.Name = this.User.name;
      }
    )
    this.Bool = false;
  }

}

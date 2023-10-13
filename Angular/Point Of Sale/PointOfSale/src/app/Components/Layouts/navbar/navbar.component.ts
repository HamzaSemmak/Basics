import { HttpErrorResponse } from '@angular/common/http';
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
        this.User = response;
        this.Name = this.User[0].name
      },
      (err: HttpErrorResponse) => {
        console.log(err);
      }
    )
    this.Bool = false;
  }

}

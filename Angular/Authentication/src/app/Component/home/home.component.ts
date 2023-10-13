import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/Service/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {
  User: any;
  Name: any;

  constructor(private Service: AuthService) {}

  ngOnInit(): void {
    this.Service.User().subscribe( res => {
      this.User = res;
      this.Name = this.User.name;
    })
  }
}

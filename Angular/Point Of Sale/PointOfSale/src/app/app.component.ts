import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {
  title: string = 'Point Of Sale';
  AuthOr404: Boolean = false;
  Home: Boolean = false;

  constructor(private location: Location) { }

  ngOnInit(): void {
    if(this.location.path().includes('auth') || this.location.path().includes('404'))
    {
      this.AuthOr404 = true;
    }
    else {
      this.Home = true;
    }
  }
}

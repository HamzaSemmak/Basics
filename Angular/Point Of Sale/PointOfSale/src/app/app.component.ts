import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {
  title: string = 'Point Of Sale';
  Bool: boolean = true;

  constructor(private location: Location) { }

  ngOnInit(): void {
    if(this.location.path().includes('auth') || this.location.path() == "*")
    {
      this.Bool = false;
    }
  }

}

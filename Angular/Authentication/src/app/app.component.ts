import { Component, DoCheck } from '@angular/core';
import { Router } from '@angular/router';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements DoCheck {
  title = 'Authentication';
  isMenuRequired: boolean = false;

  constructor(private Route: Router) {}

  ngDoCheck(): void {
    let CurrentUrl = this.Route.url;
    if(CurrentUrl == '/login' || CurrentUrl == '/register')
    {
      this.isMenuRequired = false;
    }
    else {
      this.isMenuRequired = true;
    }
  }
}

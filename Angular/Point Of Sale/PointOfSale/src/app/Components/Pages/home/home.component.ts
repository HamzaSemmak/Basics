import { Component, OnInit } from '@angular/core';
import { ToastService } from 'src/app/Services/Toast/toast.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {

  constructor(private Toast: ToastService) {}

  ngOnInit(): void {
    this.Toast.warning("warning message test please work");
  }
}

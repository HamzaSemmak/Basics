import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'error',
  templateUrl: './error.component.html',
  styleUrls: ['./error.component.css']
})
export class ErrorComponent implements OnInit {

  @Input() message: string;

  constructor() { }

  ngOnInit(): void {
    return;
  }
  
}

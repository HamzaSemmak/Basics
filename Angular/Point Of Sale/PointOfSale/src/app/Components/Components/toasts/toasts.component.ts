import { Component, Input, OnInit  } from '@angular/core';

@Component({
  selector: 'app-toasts',
  templateUrl: './toasts.component.html',
  styleUrls: ['./toasts.component.css']
})
export class ToastsComponent implements OnInit {

  @Input() message: string;

  constructor() { }

  ngOnInit(): void {
    return;
  }
  
}

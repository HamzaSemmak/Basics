import { Component, Input, OnInit } from '@angular/core';
import { User } from 'src/app/Modules/Model/Users';

@Component({
  selector: 'user-show',
  templateUrl: './show.component.html',
  styleUrls: ['./show.component.css']
})

export class ShowComponent implements OnInit {
  @Input() User: User;
  Modal: HTMLElement | null

  constructor() {}

  ngOnInit(): void {
  }

  CloseModal(): void {
    this.Modal = document.querySelector('.Modal')
    this.Modal?.classList.remove('showModal');
  }
}

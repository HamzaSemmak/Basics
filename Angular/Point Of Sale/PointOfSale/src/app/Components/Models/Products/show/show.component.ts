import { Component, OnInit, Input } from '@angular/core';
import { Products } from 'src/app/Modules/Model/Products';

@Component({
  selector: 'product-show',
  templateUrl: './show.component.html',
  styleUrls: ['./show.component.css']
})

export class ShowComponent implements OnInit {
  @Input() Product: Products;
  Modal: HTMLElement | null

  constructor() {}

  ngOnInit(): void {
    
  }

  CloseModal(): void {
    this.Modal = document.querySelector('.Modal')
    this.Modal?.classList.remove('showModal');
  }
}

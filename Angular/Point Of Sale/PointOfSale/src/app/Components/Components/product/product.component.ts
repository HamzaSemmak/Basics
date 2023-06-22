import { Component, OnInit, Input } from '@angular/core';
import { Products } from 'src/app/Modules/Model/Products';


@Component({
  selector: 'product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {
  @Input() Product: Products;

  constructor() { return; }

  ngOnInit(): void {
    
  }

}

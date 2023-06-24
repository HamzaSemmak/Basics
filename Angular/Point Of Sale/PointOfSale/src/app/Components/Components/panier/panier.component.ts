import { Component, OnInit } from '@angular/core';
import { Products } from 'src/app/Modules/Model/Products';

@Component({
  selector: 'panier',
  templateUrl: './panier.component.html',
  styleUrls: ['./panier.component.css']
})
export class PanierComponent implements OnInit  {
  Products: Products[];

  constructor() {}

  ngOnInit(): void {
    return;
  }

  increase(Product: Products): void {
    console.log(Product);
  }

  decrease(Product: Products): void {
    console.log(Product);
  }

}

import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})

export class ProductsComponent implements OnInit {
  Products: Array<any>;

  constructor() {}

  ngOnInit(): void {
    this.Products = [
      {id: 1, name: "PC", price: 4500 },
      {id: 2, name: "Phone", price: 5000 },
      {id: 3, name: "Shoes", price: 350 }
    ]
  }

  handleDeleteProducts(item: any): void {
    this.Products = this.Products.filter(p => p.id != item.id )
  }

}

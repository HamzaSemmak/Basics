import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/Model/products';
import { ProductsService } from 'src/app/Services/Product/products.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})

export class ProductsComponent implements OnInit {
  Products: Array<Product>; 
  ErrorMsg: string = '';

  constructor(private productService: ProductsService) {}

  ngOnInit(): void {
    this.productService.getAllProducts().subscribe(
      {
        next : (response) => {
          this.Products = response
        },
        error : (error) => {
          this.ErrorMsg = error;
        }
    })
  }

  handleDeleteProducts(item: Product): void {
    let config = confirm("Are you sure!");
    if(!config) return;
    
    this.productService.deleteProducts(item).subscribe(
      (response) => this.Products = response
    );
  }

}

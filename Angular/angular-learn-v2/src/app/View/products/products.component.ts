import { Component, OnInit } from '@angular/core';
import { ProductsService } from 'src/app/Services/Product/products.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})

export class ProductsComponent implements OnInit {
  Products: Array<any>; 
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

  handleDeleteProducts(item: any): void {
    this.productService.deleteProducts(item).subscribe(
      (response) => this.Products = response
    );
  }

}

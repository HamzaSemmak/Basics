import { Component, OnInit } from '@angular/core';
import { Products } from 'src/app/Modules/Model/Products';
import { ProductsService } from 'src/app/Services/Products/products.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {
  Products: Products[];

  constructor(private ProductsService: ProductsService) { }
  
  ngOnInit(): void {
    this.ProductsService.getProducts().subscribe(
      (response) => this.Products = response
    );
  }

}

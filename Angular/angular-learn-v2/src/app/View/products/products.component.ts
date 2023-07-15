import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Product } from 'src/app/Model/products';
import { ProductsService } from 'src/app/Services/Product/products.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})

export class ProductsComponent implements OnInit {
  Products: Array<Product>; 
  currentPage: number = 0;
  Pagesize: number = 5;
  PageTotal: number = 5;
  ErrorMsg: string = '';
  SearchForm: FormGroup

  constructor(private productService: ProductsService, private FormBuilder: FormBuilder) {}

  ngOnInit(): void {
    // this.productService.getPageProducts(this.currentPage, this.Pagesize).subscribe(
    //   {
    //     next : (response) => {
    //       this.Products = response.items
    //       this.PageTotal= response.page
    //     },
    //     error : (error) => {
    //       this.ErrorMsg = error;
    //     }
    // })

    this.handleProducts();

    this.SearchForm = this.FormBuilder.group({
      Keyword: this.FormBuilder.control('', [Validators.required])
    })
  }

  handleProducts(): void {
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

  handleSerach(): void {
    if(this.SearchForm.valid) {
      this.productService.searchProduct(this.SearchForm.value.Keyword).subscribe(
        {
          next : (response) => {
            this.Products = response
          },
          error : (error) => {
            this.ErrorMsg = error
          }
        }
      )
    }
    else {
      this.ErrorMsg = "Invalid Form";
    }
  }

  handleDeleteProducts(item: Product): void {
    // let config = confirm("Are you sure!");
    // if(!config) return;
    
    this.productService.deleteProducts(item).subscribe(
      (response) => this.Products = response
    );
  }

  handleSetPromotion(item: Product): void {
    this.productService.updateProducts(item).subscribe(
      {
        next : (Response) => {
          item = Response
        },
        error : (error) => {
          this.ErrorMsg = error
        }
      }
    )
  }

}

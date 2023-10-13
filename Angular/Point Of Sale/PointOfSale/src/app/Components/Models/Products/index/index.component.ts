import { Component, OnInit } from '@angular/core';
import { Products } from 'src/app/Modules/Model/Products';
import { ProductsService } from 'src/app/Services/Products/products.service';
import { ToastService } from 'src/app/Services/Toast/toast.service';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})

export class IndexComponent implements OnInit {
  Products: Products[] = [];
  Product: Products;
  Modal: HTMLElement | null

  constructor(private productService: ProductsService, private toast: ToastService) {}

  ngOnInit(): void {
    this.productService.getProducts().subscribe(
      (response) => this.Products = response
    )

    this.Product = {
      "id": 5,
      "title": "Campus 00s Grey White",
      "description": "Campus 00s Grey White with white logo stripes and thick white midsole",
      "price": 150,
      "stock": 99,
      "category": "adidas",
      "image": "assets/images/baskets-campus-00s-grey-white-pour-homme-et-femme-disponible-sur-kikikickz-687197_800x.jpg"
    }
  }

  ngDeleteProduct(item: Products): void {
    this.productService.delete(item).subscribe(
      () => {
        this.Products = this.Products.filter(u => u.id != item.id);
      }
    )
    this.toast.success('Record has been successfully deleted') 
  }

  ngUpdateProduct(item: Products): void {
    console.log(item);
  }

  ngShowProduct(item: Products): void {
    this.Modal = document.querySelector('.Modal')
    this.Modal?.classList.add('showModal');
    this.Product = item;
  }

}

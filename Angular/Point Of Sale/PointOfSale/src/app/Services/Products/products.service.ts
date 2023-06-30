import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, elementAt } from 'rxjs';
import { ApiProducts } from 'src/app/Modules/Config/Api';
import { Products } from 'src/app/Modules/Model/Products';
import { basket } from 'src/app/Modules/Model/basket';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
  })
}


@Injectable({
  providedIn: 'root'
})
export class ProductsService {
  Product: Products;

  constructor(private HttpClient: HttpClient) { }

  getProducts(): Observable<Products[]> {
    return this.HttpClient.get<Products[]>(ApiProducts, httpOptions);
  }

  UpdateProductsPassedOnBasket(basket: basket[]): void {
    Object.values(basket).forEach(item => {
      this.Product = item.product;
      this.Product.stock -= item.quantite;
      this.HttpClient.put(`${ApiProducts}/${this.Product.id}`, this.Product, httpOptions).subscribe(
        (response) => {
          return response;
        }
      )
    })
  }
}

import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiBasket } from 'src/app/Modules/Config/Api';
import { basket } from 'src/app/Modules/Model/basket';
import { Products } from 'src/app/Modules/Model/Products';
import { User } from 'src/app/Modules/Model/Users';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
  })
}


@Injectable({
  providedIn: 'root'
})
export class BasketService {
  Basket: basket;

  constructor(private HttpClient: HttpClient) { }

  getBasket(): Observable<basket[]> {
    return this.HttpClient.get<basket[]>(ApiBasket , httpOptions);
  }

  setProductToBasket(Product: Products, User: User): void {
    this.Basket = {
      product: Product.id,
      date: new Date(),
      user: User.id
    }
    this.HttpClient.post(ApiBasket, this.Basket, httpOptions);
  }

}

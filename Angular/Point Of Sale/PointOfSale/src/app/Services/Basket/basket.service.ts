import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiBasket } from 'src/app/Modules/Config/Api';
import { basket } from 'src/app/Modules/Model/basket';
import { Products } from 'src/app/Modules/Model/Products';
import { User } from 'src/app/Modules/Model/Users';
import { Keys } from 'src/app/Modules/Config/Config';

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
  dateOfToday: any;
  id: number = Math.floor(Math.random() * 100000);

  constructor(private HttpClient: HttpClient) { 
    var date = new Date();
    this.dateOfToday = date.toISOString().split('T')[0];
  }

  getBasket(): Observable<basket[]> {
    return this.HttpClient.get<basket[]>(`${ApiBasket}?userKey=${sessionStorage.getItem(Keys)}` , httpOptions);
  }

  setProductToBasket(Product: Products): Observable<basket> {
    this.Basket = {
      id: this.id,
      product: Product,
      date: this.dateOfToday,
      userKey: sessionStorage.getItem(Keys)?.toString(),
      quantite: 1
    }
    return this.HttpClient.post<basket>(ApiBasket, this.Basket, httpOptions);
  }

}

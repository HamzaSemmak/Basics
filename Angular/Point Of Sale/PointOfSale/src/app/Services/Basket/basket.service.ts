import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiBasket } from 'src/app/Modules/Config/Api';
import { basket } from 'src/app/Modules/Model/basket';
import { Products } from 'src/app/Modules/Model/Products';
import { User } from 'src/app/Modules/Model/Users';
import { Keys } from 'src/app/Modules/Config/Config';
import { Router } from '@angular/router';

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
  Baskets: basket[];
  dateOfToday: any;
  id: number = Math.floor(Math.random() * 100000);

  constructor(private HttpClient: HttpClient, private router: Router) { 
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
      quantite: 1,
      price: Product.price
    }
    return this.HttpClient.post<basket>(ApiBasket, this.Basket, httpOptions);
  }

  clearBasket(): Observable<basket[]> {
    this.getBasket().subscribe(
      (Response) => {
        this.Baskets = Object.values(Response);
        this.Baskets.forEach(element => {
          this.HttpClient.delete(`${ApiBasket}/${element.id}`, httpOptions).subscribe();
        });
      }
    )
    return this.getBasket();
  }

  deleteItemFromBasket(basket: basket): Observable<basket> {
    return this.HttpClient.delete<basket>(`${ApiBasket}/${basket.id}`, httpOptions);
  }

  UpdateItemInBaskets(basket: basket): Observable<basket> {
    return this.HttpClient.put<basket>(`${ApiBasket}/${basket.id}`, basket, httpOptions);
  }
}

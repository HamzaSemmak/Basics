import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiOrders } from 'src/app/Modules/Config/Api';
import { basket } from 'src/app/Modules/Model/basket';
import { Orders } from 'src/app/Modules/Model/Order';
import { Keys } from 'src/app/Modules/Config/Config';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
  })
}

@Injectable({
  providedIn: 'root'
})

export class OrdersService {
  Orders: Orders;
  dateOfToday: any;

  constructor(private HttpClient: HttpClient) {
    var date = new Date();
    this.dateOfToday = date.toISOString().split('T')[0];
   }

  setOrder(Baskets: basket[], ID: number): Observable<Orders> {
    this.Orders = {
      id: ID,
      basket: Baskets,
      date: this.dateOfToday,
      userKey: sessionStorage.getItem(Keys)?.toString()
    };
    return this.HttpClient.post<Orders>(ApiOrders, this.Orders, httpOptions);
  }

}

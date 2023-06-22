import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiProducts } from 'src/app/Modules/Config/Api';
import { Products } from 'src/app/Modules/Model/Products';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
  })
}


@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  constructor(private HttpClient: HttpClient) { }

  getProducts(): Observable<Products[]> {
    return this.HttpClient.get<Products[]>(ApiProducts, httpOptions);
  }
}

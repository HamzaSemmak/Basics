import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Category } from 'src/app/Modules/Model/Category';
import { ApiCategorys, ApiProducts } from 'src/app/Modules/Config/Api';
import { Products } from 'src/app/Modules/Model/Products';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
  })
}


@Injectable({
  providedIn: 'root'
})

export class CategoryService {

  constructor(private HttpClient: HttpClient) { }

  getCategory(): Observable<Category[]> {
    return this.HttpClient.get<Category[]>(ApiCategorys, httpOptions);
  }

  findByCategory(Category: string): Observable<Products[]> {
    return this.HttpClient.get<Products[]>(`${ApiProducts}?category=${Category}`, httpOptions);
  }

}

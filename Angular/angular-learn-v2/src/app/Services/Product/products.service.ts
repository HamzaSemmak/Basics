import { Injectable } from '@angular/core';
import { Observable, of, throwError } from 'rxjs';
import { Product } from 'src/app/Model/products';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {
  private Products: Array<Product>

  constructor() {
    this.Products = [
      {id: 1, name: "PC", price: 4500 },
      {id: 2, name: "Phone", price: 5000 },
      {id: 3, name: "Shoes", price: 350 }
    ]
  }

  public getAllProducts(): Observable<Array<Product>> {
    if (this.Products.length <= 0) return throwError(() => new Error("No Available data"))
    else return of(this.Products)
  }

  public deleteProducts(item: Product): Observable<Array<Product>> {
    this.Products = this.Products.filter((p) => p.id != item.id)
    return of(this.Products)
  }

}

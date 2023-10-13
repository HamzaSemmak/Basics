import { Injectable } from '@angular/core';
import { UUID } from 'angular2-uuid';
import { Observable, of, throwError } from 'rxjs';
import { Paginate } from 'src/app/Model/Paginate';
import { Product } from 'src/app/Model/products';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {
  private Products: Array<Product>

  constructor() {
    this.Products = [
      {id: UUID.UUID(), name: "Computer", price: 4500, promotion: true },
      {id: UUID.UUID(), name: "Phone", price: 5000,  promotion: false },
      {id: UUID.UUID(), name: "Shoes", price: 350,  promotion: true }
    ]
    for (let index = 0; index < 10; index++) {
      this.Products.push( {id: UUID.UUID(), name: "Computer", price: 4500, promotion: true })
      this.Products.push( {id: UUID.UUID(), name: "Phone", price: 5000,  promotion: false })
      this.Products.push( {id: UUID.UUID(), name: "Shoes", price: 350,  promotion: true  })
    }
  }

  public getAllProducts(): Observable<Array<Product>> {
    if (this.Products.length <= 0) return throwError(() => new Error("No Available data"))
    else return of(this.Products)
  }

  public getPageProducts(page: number, size: number): Observable<Paginate> {
    let index = page * size
    let total = ~~this.Products/size
    if(this.Products.length % size != 0) {
      total++
    }
    let pagePdt = this.Products.slice(index, index+size)

    return of(
      { items: pagePdt, page: page, size: size, total: total}
    )
  }

  public deleteProducts(item: Product): Observable<Array<Product>> {
    this.Products = this.Products.filter((p) => p.id != item.id)
    return of(this.Products)
  }

  public updateProducts(item: Product): Observable<Product> {
    let Product = this.Products.find(p => p.id == item.id)
    if(Product == undefined)
    {
       return throwError(() => new Error("Product undefined"))
    }
    else {
      Product.promotion = !Product.promotion;
      return of(Product);
    }
  }

  public searchProduct(value: string): Observable<Array<Product>> {
    return of(
      this.Products.filter( p => p.name.includes(value) )
    );
  }

}

import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Keys } from 'src/app/Modules/Config/Config';
import { Products } from 'src/app/Modules/Model/Products';
import { basket as Basket, basket } from 'src/app/Modules/Model/basket';
import { BasketService } from 'src/app/Services/Basket/basket.service';

@Component({
  selector: 'panier',
  templateUrl: './panier.component.html',
  styleUrls: ['./panier.component.css']
})

export class PanierComponent implements OnInit  {
  Products: any[];
  Baskets: Basket[];
  Total: number = 0;
  SubTotal: number = 0;
  DisCountSales: number = 50;
  TotalSalesTax: number = 20;

  constructor(private basketService: BasketService, private router: Router) {}

  ngOnInit(): void {
    this.basketService.getBasket().subscribe(
      (response) => {
        this.Baskets = response;
        this.Products = Object.values(response);
        this.Products.forEach(element => {
          this.SubTotal += element.price
        });
        if(this.SubTotal == 0)
        {
          this.Total = 0;
          return;
        }
        this.Total = this.SubTotal - 30;
      }
    )
  }

  ngValidatePayement(): void {
    if(Object.values(this.Baskets).length <= 0)
      return;

    //Traitment
    this.router.navigate([`payement/${sessionStorage.getItem(Keys)}/${Object.values(this.Baskets).toString()}/validate`])
  }

  increase(Product: Basket): void {
    if(Product.quantite >= Product.product.stock)
    {
      return;
    }
    else {
      this.SubTotal = this.SubTotal - Product.price
      this.Total = this.SubTotal - 30;
      Product.quantite++;
      Product.price = Product.product.price * Product.quantite;
      this.UpdateBasket(Product);
    }
    this.SubTotal += Product.price
    this.Total = this.SubTotal - 30;
  }

  decrease(Product: Basket): void {
    if(Product.quantite < 2)
    {
      return;
    }
    else {
      this.SubTotal = this.SubTotal - Product.price
      this.Total = this.SubTotal - 30;
      Product.quantite--;
      Product.price = Product.product.price * Product.quantite;
      this.UpdateBasket(Product);
    }
    this.SubTotal += Product.price
    this.Total = this.SubTotal - 30;
  }

  ngClearBasket(): void {
    this.basketService.clearBasket();
    this.Baskets = [];
    this.Total = 0;
    this.SubTotal = 0;
  }

  ngDeleteItemFromBasket(item: basket): void {
    this.basketService.deleteItemFromBasket(item).subscribe(() => {
      this.Baskets =  this.Baskets.filter(t => t.id != item.id)
    })
    this.SubTotal -= item.price;
    if(this.SubTotal == 0)
    {
      this.Total = 0;
      return;
    }
    this.Total = this.SubTotal - 30;
  }

  UpdateBasket(Basket: Basket): void {
    this.basketService.UpdateItemInBaskets(Basket).subscribe(
      (response) => {
        return response;
      }
    )
  }
}
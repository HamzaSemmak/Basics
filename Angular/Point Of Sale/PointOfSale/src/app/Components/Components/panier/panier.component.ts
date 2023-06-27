import { Component, OnInit } from '@angular/core';
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

  constructor(private basketService: BasketService) {}

  ngOnInit(): void {
    this.basketService.getBasket().subscribe(
      (response) => {
        this.Baskets = response;
      }
    )
  }

  increase(Product: Basket): void {
    if(Product.quantite >= Product.product.stock)
    {
      return;
    }
    else {
      Product.quantite++;
      Product.price = Product.product.price * Product.quantite;
      this.UpdateBasket(Product);
    }
  }

  decrease(Product: Basket): void {
    if(Product.quantite < 2)
    {
      return;
    }
    else {
      Product.quantite--;
      Product.price = Product.product.price * Product.quantite;
      this.UpdateBasket(Product);
    }
  }

  ngClearBasket(): void {
    this.basketService.clearBasket();
    this.Baskets = [];
    //Total SubTotal 
  }

  ngDeleteItemFromBasket(item: basket): void {
    this.basketService.deleteItemFromBasket(item).subscribe(() => {
      this.Baskets =  this.Baskets.filter(t => t.id != item.id)
    })
  }

  UpdateBasket(Basket: Basket): void {
    this.basketService.UpdateItemInBaskets(Basket).subscribe(
      (response) => {
        return response;
      }
    )
  }
}
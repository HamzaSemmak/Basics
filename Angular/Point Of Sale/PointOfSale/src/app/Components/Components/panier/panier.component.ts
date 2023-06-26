import { Component, OnInit } from '@angular/core';
import { basket } from 'src/app/Modules/Model/basket';
import { BasketService } from 'src/app/Services/Basket/basket.service';

@Component({
  selector: 'panier',
  templateUrl: './panier.component.html',
  styleUrls: ['./panier.component.css']
})

export class PanierComponent implements OnInit  {
  Products: any[];
  Baskets: basket[];
  Total: number = 0;
  SubTotal: number = 0;
  DisCountSales: number = 50;
  TotalSalesTax: number = 20;

  constructor(private basketService: BasketService) {}

  ngOnInit(): void {
    this.basketService.getBasket().subscribe(
      (response) => {
        this.Baskets = response;

        this.Products = Object.values(response);
        if(this.Products.length <= 0)
        {
          return;
        }
        this.Products.forEach(element => {
          this.SubTotal += element.product.price;
        });

        this.CalcTotal(this.SubTotal);
      }
    )
  }

  CalcTotal(SubTotal: number): number {
    return this.Total = SubTotal + this.TotalSalesTax - this.DisCountSales;
  }

  increase(Product: basket): void {
    if(Product.quantite >= Product.product.stock) {
      return;
    }
    else {
      Product.quantite++;
      Product.price = Product.quantite * Product.product.price;

      this.SubTotal += Product.price - Product.product.price
      this.CalcTotal(this.SubTotal);
    }
  }

  decrease(Product: basket): void {
    if(Product.quantite < 2) {
      return;
    }
    else {
      Product.quantite--;
      Product.price -= Product.product.price;

      this.SubTotal -= Product.price;
      this.CalcTotal(this.SubTotal);
    } 
  }

  ngClearBasket(): void {
    this.basketService.clearBasket();
  }

  ngDeleteItemFromBasket(item: basket): void {
    this.basketService.deleteItemFromBasket(item).subscribe(() => {
      this.Baskets =  this.Baskets.filter(t => t.id != item.id)
      window.location.reload();
    })
  }

}

//To Be Continue Here...
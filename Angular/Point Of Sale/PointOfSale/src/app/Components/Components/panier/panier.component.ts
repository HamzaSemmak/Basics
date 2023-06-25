import { Component, OnInit } from '@angular/core';
import { Products } from 'src/app/Modules/Model/Products';
import { basket } from 'src/app/Modules/Model/basket';
import { BasketService } from 'src/app/Services/Basket/basket.service';

@Component({
  selector: 'panier',
  templateUrl: './panier.component.html',
  styleUrls: ['./panier.component.css']
})
export class PanierComponent implements OnInit  {
  Products: Products[];
  Baskets: basket[];

  constructor(private basketService: BasketService) {}

  ngOnInit(): void {
    this.basketService.getBasket().subscribe(
      (response) => this.Baskets = response
    )
  }

  increase(Product: basket): void {
    Product.quantite++
  }

  decrease(Product: basket): void {
    Product.quantite--
  }

}

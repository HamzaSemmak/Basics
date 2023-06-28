import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { basket } from 'src/app/Modules/Model/basket';
import { BasketService } from 'src/app/Services/Basket/basket.service';

@Component({
  selector: 'app-payement',
  templateUrl: './payement.component.html',
  styleUrls: ['./payement.component.css']
})

export class PayementComponent implements OnInit {
  Baskets: basket[] = [];
  Total: number = 0;

  constructor(
    private Router: Router,
    private ActivatedRoute: ActivatedRoute,
    private BasketService: BasketService
  ) { }

  ngOnInit(): void {
    this.BasketService.getBasket().subscribe(
      (response) => {
        this.Baskets = response;
        Object.values(this.Baskets).forEach(element => {
          this.Total = this.Total + element.price;
        });
      }
    )
  }
}

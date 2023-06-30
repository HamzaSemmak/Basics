import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Keys } from 'src/app/Modules/Config/Config';
import { basket } from 'src/app/Modules/Model/basket';
import { BasketService } from 'src/app/Services/Basket/basket.service';
import { ToastService } from 'src/app/Services/Toast/toast.service';

@Component({
  selector: 'app-payement',
  templateUrl: './payement.component.html',
  styleUrls: ['./payement.component.css']
})

export class PayementComponent implements OnInit {
  Baskets: basket[] = [];
  OrderID: number = 0
  Total: number = 0;
  SubTotal: number = 0;
  TotalProduct: number = 0;
  DisCountSales: number = 50;
  TotalSalesTax: number = 20;
  
  constructor(
    private Router: Router,
    private ActivatedRoute: ActivatedRoute,
    private BasketService: BasketService,
    private Toast: ToastService
  ) { 
    this.OrderID = this.generateOrderID();
  }

  ngOnInit(): void {
    if(this.ActivatedRoute.snapshot.params['Key'] != sessionStorage.getItem(Keys)) {
      this.Router.navigate(['/']);
      this.Toast.error("You d'ont have access to this page");
    }
    this.BasketService.getBasket().subscribe(
      (response) => {
        this.Baskets = response;
        this.TotalProduct = this.Baskets.length;
        Object.values(this.Baskets).forEach(element => {
          this.SubTotal = this.SubTotal + element.price;
        });
        this.Total = this.SubTotal - 30;
      }
    )
  }
  
  generateOrderID(): number {
    return Math.floor(Math.random() * 999);
  }
}

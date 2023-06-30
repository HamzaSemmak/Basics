import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Keys } from 'src/app/Modules/Config/Config';
import { Products } from 'src/app/Modules/Model/Products';
import { basket } from 'src/app/Modules/Model/basket';
import { BasketService } from 'src/app/Services/Basket/basket.service';
import { ToastService } from 'src/app/Services/Toast/toast.service';
import * as html2pdf from 'html2pdf.js';

@Component({
  selector: 'app-payement',
  templateUrl: './payement.component.html',
  styleUrls: ['./payement.component.css']
})

export class PayementComponent implements OnInit {
  Baskets: basket[] = [];
  Products: Products[] = [];
  OrderID: number = 0
  Total: number = 0;
  SubTotal: number = 0;
  TotalProduct: number = 0;
  DisCountSales: number = 50;
  TotalSalesTax: number = 20;
  Date: string;
  Spinner: boolean = false;
  filename: string = 'Invoice'
  Options: any;
  Bill: HTMLElement | null;

  constructor(
    private Router: Router,
    private ActivatedRoute: ActivatedRoute,
    private BasketService: BasketService,
    private Toast: ToastService,
  ) { 
    this.OrderID = this.generateOrderID();
    this.Date = this.getCurrentDate();
    this.Bill = document.querySelector('.bill')
    this.Options =  {
      margin:        [0.5, 0.4, 0, 0.6],
      filename:     `${this.filename}`,
      image:        { type: 'jpeg', quality: 0.98 },
      html2canvas:  { scale: 2, bottom: 0 },
      jsPDF:        { unit: 'in', format: 'letter', orientation: 'portrait' }
    }
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

  html2pdf(): void {
    try {
      //converts to PDF
      html2pdf()
      .set(this.Options)
      .from(this.Bill)
      .save()

      console.log('Good');

    } catch (error) {
      console.log(error);
    }
  }

  generateOrderID(): number {
    return Math.floor(Math.random() * 9999999);
  }

  getCurrentDate(): string {
    var dt = new Date();
    return dt.toString();
  }

  ngValidateOrder() :void {
    this.Spinner = true;
    // this.orderService.setOrder(this.Baskets, this.OrderID).subscribe(
    //   (Order) => {
    //     this.productService.UpdateProductsPassedOnBasket(this.Baskets);
    //     this.BasketService.clearBasket().subscribe(
    //       (Basket) => {
    //         console.log(Order);
    //         console.log(Basket);
    //       }
    //     )
    //   }
    // )
    this.html2pdf();
    setTimeout(() => {
      this.Spinner = false;
      // this.Router.navigate(['/']);
    }, 7000);
  }

  getProducts(): Products[] {
    Object.values(this.Baskets).forEach(element => {
      this.Products.push(element.product);
    })
    return this.Products;
  }
}
//Bill Print 
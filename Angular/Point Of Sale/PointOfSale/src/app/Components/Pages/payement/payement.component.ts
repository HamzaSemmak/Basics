import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Keys } from 'src/app/Modules/Config/Config';
import { Products } from 'src/app/Modules/Model/Products';
import { basket } from 'src/app/Modules/Model/basket';
import { BasketService } from 'src/app/Services/Basket/basket.service';
import { ToastService } from 'src/app/Services/Toast/toast.service';
import jsPDF from 'jspdf';
import html2canvas from 'html2canvas';
import { OrdersService } from 'src/app/Services/Orders/orders.service';
import { ProductsService } from 'src/app/Services/Products/products.service';


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
  Invoice: string;
  Bill: any;
  jsPDF: jsPDF = new jsPDF();

  constructor(
    private Router: Router,
    private ActivatedRoute: ActivatedRoute,
    private BasketService: BasketService,
    private Toast: ToastService,
    private orderService: OrdersService,
    private productService: ProductsService
  ) { 
    this.OrderID = this.generateOrderID();
    this.Date = this.getCurrentDate();
    this.Invoice = `Invoice-${this.OrderID}.pdf`;
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
      this.Bill = document.querySelector('.bill');
      html2canvas(this.Bill).then((canvas) => {
        const imgData = canvas.toDataURL('image/png');
        const imgProps = this.jsPDF.getImageProperties(imgData);
        const pdfWidth = this.jsPDF.internal.pageSize.getWidth();
        const pdfHeight = (imgProps.height * pdfWidth) / imgProps.width;
        this.jsPDF.addImage(imgData, 'PNG', 0, 0, pdfWidth, pdfHeight);
        this.jsPDF.save(this.Invoice);
      });
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
    this.orderService.setOrder(this.Baskets, this.OrderID).subscribe(
      (Order) => {
        this.productService.UpdateProductsPassedOnBasket(this.Baskets);
        this.BasketService.clearBasket().subscribe(
          (Basket) => {
            this.html2pdf();
          }
        )
      }
    )
    setTimeout(() => {
      this.Spinner = false;
      this.Router.navigate(['/']);
      this.Toast.success("You've successflly placed the order.");
    }, 7000);
  }

  getProducts(): Products[] {
    Object.values(this.Baskets).forEach(element => {
      this.Products.push(element.product);
    })
    return this.Products;
  }
}
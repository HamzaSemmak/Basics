import { Component, OnInit } from '@angular/core';
import { Orders } from 'src/app/Modules/Model/Order';
import { OrdersService } from 'src/app/Services/Orders/orders.service';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})

export class IndexComponent implements OnInit {
  Orders: Orders[] = [];

  constructor(
    private OrderService: OrdersService
  ) {}

  ngOnInit(): void {
    this.OrderService.all().subscribe(
      (response) => this.Orders = response
    )
  }

}

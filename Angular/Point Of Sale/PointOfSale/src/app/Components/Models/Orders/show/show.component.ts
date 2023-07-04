import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Orders } from 'src/app/Modules/Model/Order';
import { User } from 'src/app/Modules/Model/Users';
import { OrdersService } from 'src/app/Services/Orders/orders.service';
import { UserService } from 'src/app/Services/User/user.service';

@Component({
  selector: 'app-show',
  templateUrl: './show.component.html',
  styleUrls: ['./show.component.css']
})

export class ShowComponent implements OnInit {
  Order: Orders;
  User: User;
  ID: number;

  constructor (
    private OrderService: OrdersService,
    private ActivateRouter: ActivatedRoute,
    private UserService: UserService
  ) {}

  ngOnInit(): void {
    this.ID = this.ActivateRouter.snapshot.params['id']
    this.OrderService.find(this.ID).subscribe(
      (response) => {
        this.Order = response;
        this.UserService.findByColumn('Key', this.Order.userKey).subscribe(
          (res) => {
            this.User = res[0]; 
          }
        )
      }
    )
  }
}

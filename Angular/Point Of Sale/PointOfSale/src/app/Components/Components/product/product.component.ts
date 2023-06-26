import { Component, OnInit, Input } from '@angular/core';
import { Products } from 'src/app/Modules/Model/Products';
import { User } from 'src/app/Modules/Model/Users';
import { AuthService } from 'src/app/Services/Auth/auth.service';
import { BasketService } from 'src/app/Services/Basket/basket.service';
import { ToastService } from 'src/app/Services/Toast/toast.service';

@Component({
  selector: 'product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {
  @Input() Product: Products;
  User: User;

  constructor(
    private basketService: BasketService,
    private authService: AuthService,
    private toast: ToastService
  ) 
  {}

  ngOnInit(): void { }

  ngOrderProduct(Product: Products): void {
    this.basketService.setProductToBasket(Product).subscribe(
      (Response) => window.location.reload()
    )
  }
}

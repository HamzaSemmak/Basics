import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Category } from 'src/app/Modules/Model/Category';
import { OwlService } from 'src/app/Services/Carousel/owl.service';
import { CategoryService } from 'src/app/Services/Category/category.service';
import { ToastService } from 'src/app/Services/Toast/toast.service';

@Component({
  selector: 'category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})

export class CategoryComponent implements OnInit {
  Categorys: Category[];

  constructor(private router: Router, private categoryService: CategoryService, private Owl: OwlService) {}

  ngOnInit(): void {
    this.categoryService.getCategory().subscribe(
      (Response) => this.Categorys = Response
    )
  }

  ngOnNavigate(item: string): void {
    if(item == "/")
    {
      this.router.navigate(["/"]).then(() => {
        setTimeout(() => { window.location.reload(); }, 0);
      })
    }
    else {
      this.router.navigate([`/products/category/${item}`]).then(() => {
        setTimeout(() => { window.location.reload(); }, 0);
      })
    }
  }
  
}

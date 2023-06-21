import { Component, OnInit } from '@angular/core';
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

  constructor(private Owl: OwlService, private Toast: ToastService, private categoryService: CategoryService) {}

  ngOnInit(): void {
    this.Owl.owl2();

    this.categoryService.getCategory().subscribe(
      (Response) => this.Categorys = Response
    )
  }
  
}

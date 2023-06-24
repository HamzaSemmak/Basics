import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { Products } from 'src/app/Modules/Model/Products';
import { CategoryService } from 'src/app/Services/Category/category.service';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {
  Category: string;
  Products: Products[];

  constructor(private router: Router, private ActivateRoute: ActivatedRoute, private categoryService: CategoryService) {}

  ngOnInit(): void {
    this.Category = this.ActivateRoute.snapshot.params['item'];

    this.categoryService.findByCategory(this.Category).subscribe(
      (response) => this.Products = response
    )
  }

}

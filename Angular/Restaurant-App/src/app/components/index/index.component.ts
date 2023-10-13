import { Component, OnInit } from '@angular/core';
import { RestoService } from 'src/app/services/resto.service';
import { Restaurant } from 'src/app/Model/Restaurant';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit{
  Restaurants: Restaurant[]

  constructor(private RestoService: RestoService) { }

  ngOnInit(): void {
    this.RestoService.All().subscribe(
        (element) => {
          this.Restaurants = element
        }
    );
  }
  
  onDelete(Restaurant: Restaurant): void {
    this.RestoService.Delete(Restaurant).subscribe(() => 
      this.Restaurants = this.Restaurants.filter(t => 
        t.id != Restaurant.id
      ) 
    )
  }
}

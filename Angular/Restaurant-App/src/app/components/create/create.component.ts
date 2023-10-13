import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Restaurant } from 'src/app/Model/Restaurant';
import { RestoService } from 'src/app/services/resto.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})

export class CreateComponent implements OnInit {
  alert: boolean = false;
  Restaurant: Restaurant[];
  Restaurants = new FormGroup({
    name: new FormControl(''),
    adress: new FormControl(''),
    email: new FormControl('')
  })

  constructor(private RestoService: RestoService, private Router: Router) {}

  ngOnInit(): void {
    this.Restaurants = new FormGroup({
      name: new FormControl(''),
      adress: new FormControl(''),
      email: new FormControl('')
    })
  }

  onSubmit(): void {
    // if() 
    const newRestaurant: Restaurant = {
      name: this.Restaurants.controls.name.value || '',
      adress: this.Restaurants.controls.adress.value || '',
      email: this.Restaurants.controls.email.value || '',
    }
    this.RestoService.Create(newRestaurant).subscribe(
      (newRestaurant) => 
      this.Restaurant.push(newRestaurant)
    );
    this.alert = !this.alert
    setTimeout(() => {
      this.Router.navigate(['/Home'])
    }, 4000);
  }
}

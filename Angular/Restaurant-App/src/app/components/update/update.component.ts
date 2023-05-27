import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Restaurant } from 'src/app/Model/Restaurant';
import { RestoService } from 'src/app/services/resto.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.css']
})
export class UpdateComponent implements OnInit {
  id: number = 0;
  alert: boolean = false;
  Restaurant: Restaurant[] = [];
  Restaurants = new FormGroup({
    name: new FormControl(''),
    adress: new FormControl(''),
    email: new FormControl('')
  })

  constructor(private RestoService: RestoService, private Router: Router, private ActivateRoute: ActivatedRoute) {}

  ngOnInit(): void {
    this.ActivateRoute.params.subscribe(params => {
      this.id = +params['id'];
    });

    this.RestoService.Select(this.id).subscribe(
      (res) => {
        this.Restaurants = new FormGroup({
          name: new FormControl(res['name']),
          adress: new FormControl(res['adress']),
          email: new FormControl(res['email'])
        })
      }
    )
  }

  onSubmit(): void {
    const newRestaurant: Restaurant = {
      name: this.Restaurants.controls.name.value || '',
      adress: this.Restaurants.controls.adress.value || '',
      email: this.Restaurants.controls.email.value || '',
    }
    this.RestoService.Update(this.id, newRestaurant).subscribe(
      (newRestaurant) => 
      this.Restaurant.filter(item => item !== newRestaurant)
    );
    this.alert = !this.alert
    setTimeout(() => {
      this.Router.navigate(['/Home'])
    }, 4000);
  }
}

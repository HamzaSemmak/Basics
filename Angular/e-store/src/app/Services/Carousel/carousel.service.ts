import { Injectable } from '@angular/core';
import { OwlOptions } from 'ngx-owl-carousel-o';

@Injectable({
  providedIn: 'root'
})
export class CarouselService {
  OwlOptions: OwlOptions;

  constructor() { }

  public owl(): OwlOptions {
    return this.OwlOptions;
  }

  /*
  *   Basics Usage 
  *   Url : https://www.npmjs.com/package/ngx-owl-carousel-o
  * 
  *   Site 
  *   Url : https://owlcarousel2.github.io/OwlCarousel2/
  */
}

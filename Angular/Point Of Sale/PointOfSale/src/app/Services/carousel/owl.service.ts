import { Injectable } from '@angular/core';

declare var $: any;


@Injectable({
  providedIn: 'root'
})
export class OwlService {

  constructor() { }

  owl(): void {
    var owl = $('.owl-carousel');
    $('.owl-carousel').owlCarousel({
      loop:true,
      item:1,
      dots: false,
      margin:0,
      autoWidth:true,
      autoplay:true,
      autoplayTimeout:5000,
      smartSpeed:450
    });
    owl.on('mousewheel', '.owl-stage', function (e: any) {
        if (e.deltaY>0) {
            owl.trigger('next.owl');
        } else {
            owl.trigger('prev.owl');
        }
        e.preventDefault();
    });   
  }
}

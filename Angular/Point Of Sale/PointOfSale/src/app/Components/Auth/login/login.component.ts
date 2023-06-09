import { Component, OnInit } from '@angular/core';

declare var $: any;

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})


export class LoginComponent implements OnInit {

  constructor() {}

  ngOnInit(): void {
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

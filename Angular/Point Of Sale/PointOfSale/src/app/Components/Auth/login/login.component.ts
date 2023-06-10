import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/Modules/Users';
import { AuthService } from 'src/app/Services/Auth/auth.service';
import { FormBuilder, Validators } from '@angular/forms'
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';


declare var $: any;

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})


export class LoginComponent implements OnInit {
  User: User;

  constructor(
    private formBuilder: FormBuilder,
    private AuthService: AuthService,
    private Router: Router
  ) {}

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

  Validators = this.formBuilder.group({
    email: this.formBuilder.control('', [Validators.required, Validators.email]),
    password: this.formBuilder.control('', Validators.required)
  });

  ngOnSubmit(): void {
    if(this.Validators.valid)
    {
      this.AuthService.login(this.Validators.value.email, this.Validators.value.password).subscribe( 
        response => {
          this.User = response;

          console.log(this.User.Key);
        },
        (error: HttpErrorResponse) => {
          console.log(`Error ${error.status} : ${error.message}`);
        }
      )
    }
    else {

    }
  }

}

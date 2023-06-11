import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/Modules/Model/Users';
import { AuthService } from 'src/app/Services/Auth/auth.service';
import { FormBuilder, Validators } from '@angular/forms'
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { Response } from 'src/app/Modules/Response/Response';

declare var $: any;

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})


export class LoginComponent implements OnInit {
  User: User;
  error: boolean = true;
  message: string;

  constructor(
    private formBuilder: FormBuilder,
    private AuthService: AuthService,
    private Router: Router
  ) {  }

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
    email: this.formBuilder.control('hamza@gmail.com', [Validators.required, Validators.email]),
    password: this.formBuilder.control('AA102374h', Validators.required),
    remenber: false
  });

  ngOnSubmit(): void {
    if(this.Validators.valid)
    {
      this.AuthService.login(this.Validators.value.email, this.Validators.value.password).subscribe( 
        response => {
          this.User = response;
          let email: any = this.Validators.value.email;
          if(Object.keys(this.User).length > 0)
          {
            this.AuthService.guard(email);
          } 
          else {
            this.ThrowError(`${Response.RESPONSE_MSG_AUTH_FAILED}`);
          }
        },
        (error: HttpErrorResponse) => {
          this.ThrowError(`Error ${error.status} : ${error.message}`);
        }
      )
    }
    else {
      this.ThrowError(Response.RESPONSE_MSG_VAILDATION_FORM);
    }
  }

  ThrowError(Msg: string): void {
    this.message = Msg;
  }

}

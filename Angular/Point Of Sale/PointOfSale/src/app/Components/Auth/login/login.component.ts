import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/Modules/Model/Users';
import { AuthService } from 'src/app/Services/Auth/auth.service';
import { FormBuilder, Validators } from '@angular/forms'
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { Response } from 'src/app/Modules/Response/Response';
import { OwlService } from 'src/app/Services/carousel/owl.service';

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
    private Owl: OwlService,
    private Router: Router
  ) {  }

  ngOnInit(): void {
    if(this.AuthService.Check())
    {
      this.Router.navigate(['/']);
    }
    this.Owl.owl();
  }

  Validators = this.formBuilder.group({
    email: this.formBuilder.control('', [Validators.required, Validators.email]),
    password: this.formBuilder.control('', Validators.required),
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

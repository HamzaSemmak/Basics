import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/Services/Auth/auth.service';
import { FormBuilder, Validators } from '@angular/forms'
import { Router, ActivatedRoute  } from '@angular/router';
import { OwlService } from 'src/app/Services/Carousel/owl.service';
import { Response  } from 'src/app/Modules/Error/Response';
import { EmailService } from 'src/app/Services/Email/email.service';
import { User } from 'src/app/Modules/Model/Users';
import { HttpErrorResponse } from '@angular/common/http';
import { elementAt } from 'rxjs';

@Component({
  selector: 'app-reset',
  templateUrl: './reset.component.html',
  styleUrls: ['./reset.component.css']
})

export class ResetComponent implements OnInit {
  error: boolean = true;
  message: string;
  email: string;
  name: string;
  User: User;

  constructor(
    private formBuilder: FormBuilder,
    private AuthService: AuthService,
    private MailService: EmailService,
    private Owl: OwlService,
    private Router: Router,
    private ActivateRoute: ActivatedRoute
  ) {  }

  ngOnInit(): void {
    if(this.AuthService.Check())
    {
      this.Router.navigate(['/']);
    }
  }

  Validators = this.formBuilder.group({
    password: this.formBuilder.control('', [Validators.required]),
    confirmPassword: this.formBuilder.control('', [Validators.required])
  });

  ngOnSubmit(): void {
    if(this.Validators.valid)
    {
      if(this.Validators.value.confirmPassword != this.Validators.value.password)
      {
        this.ThrowError(Response.RESPONSE_MSG_AUTH_PASSWORD_MATCH);
      }
      else {
        //change password 
        console.log("here");
      }
    }
    else {
      this.ThrowError(Response.RESPONSE_MSG_VAILDATION_FORM)
    }
  }

  ThrowError(Msg: string): void {
    this.message = Msg;
  }
}

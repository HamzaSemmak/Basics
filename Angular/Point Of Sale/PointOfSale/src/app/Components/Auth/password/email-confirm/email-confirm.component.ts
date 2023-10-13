import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/Services/Auth/auth.service';
import { FormBuilder, Validators } from '@angular/forms'
import { Router, ActivatedRoute  } from '@angular/router';
import { OwlService } from 'src/app/Services/Carousel/owl.service';
import { Response  } from 'src/app/Modules/Error/Response';
import { EmailService } from 'src/app/Services/Email/email.service';
import { EMAIL_CODE_VEREFICATION } from 'src/app/Modules/Config/Config';
import { HttpErrorResponse } from '@angular/common/http';
import { User } from 'src/app/Modules/Model/Users';

@Component({
  selector: 'app-email-confirm',
  templateUrl: './email-confirm.component.html',
  styleUrls: ['./email-confirm.component.css']
})
export class EmailConfirmComponent implements OnInit {
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

    this.email = this.ActivateRoute.snapshot.params['email'];

    this.AuthService.ForgotPassword(this.email).subscribe(
      response => {
        if(Object.keys(response).length > 0)
        {
          this.User = response;
        } else {
          this.Router.navigate(['/auth/login']);
        }
      },
      (error: HttpErrorResponse) => {
       console.log(error);
      }
    )
  }

  Validators = this.formBuilder.group({
    code: this.formBuilder.control('', [Validators.pattern("^[0-9]*$")])
  });

  ngOnSubmit(): void {
    if(this.Validators.value.code == "" || this.Validators.value.code == null)
    {
      this.ThrowError(Response.RESPONSE_MSG_VAILDATION_EMPTY);
    }
    else {
      if(this.Validators.controls.code.errors) {
        this.ThrowError(Response.RESPONSE_MSG_VAILDATION_NUMERIC);
      }
      else {
        if(this.Validators.value.code == EMAIL_CODE_VEREFICATION)
        {
          localStorage.setItem("mail", this.email);
          this.Router.navigate(['auth/forget-password/password/reset']);
        }
        else {
          this.ThrowError(Response.RESPONSE_MSG_AUTH_CODE_RESPONSE_INCORRECT);
        }
      }
    }
  }

  ngSendEmail(): void {
    this.MailService.sendEmail(this.email, EMAIL_CODE_VEREFICATION.toString(), this.name);
  }

  ThrowError(Msg: string): void {
    this.message = Msg;
  }
}

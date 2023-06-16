import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/Services/Auth/auth.service';
import { FormBuilder, Validators } from '@angular/forms'
import { Router, ActivatedRoute  } from '@angular/router';
import { OwlService } from 'src/app/Services/Carousel/owl.service';
import { Response  } from 'src/app/Modules/Error/Response';
import { EmailService } from 'src/app/Services/Email/email.service';
import { EMAIL_CODE_VEREFICATION } from 'src/app/Modules/Config/Config';
import { HttpErrorResponse } from '@angular/common/http';
import { ToastService } from 'src/app/Services/Toast/toast.service';
import { User } from 'src/app/Modules/Model/Users';

@Component({
  selector: 'app-email',
  templateUrl: './email.component.html',
  styleUrls: ['./email.component.css']
})
export class EmailComponent implements OnInit {
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
    private ActivateRoute: ActivatedRoute,
    private ToastService: ToastService
  ) {  }

  ngOnInit(): void {
    if(this.AuthService.Check())
    {
      this.Router.navigate(['/']);
    }
    this.Owl.owl();

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
    email: this.formBuilder.control('', [Validators.email])
  });

  ngOnSubmit(): void {
    if(this.Validators.value.email == "" || this.Validators.value.email == null)
    {
      this.ThrowError(Response.RESPONSE_MSG_VAILDATION_EMPTY);
    }
    else {
      if(this.Validators.controls.email.errors) {
        this.ThrowError(Response.RESPONSE_MSG_VAILDATION_EMAIL);
      }
      else {
        var mail: string = this.Validators.value.email;
        this.MailService.sendEmail(mail, EMAIL_CODE_VEREFICATION.toString(), this.name);
        this.Router.navigate([`/auth/forget-password/email-confirm/${this.email}`]);
      }
    }
  }

  ThrowError(Msg: string): void {
    this.message = Msg;
  }
}

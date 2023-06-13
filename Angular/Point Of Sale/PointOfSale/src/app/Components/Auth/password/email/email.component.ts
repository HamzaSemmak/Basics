import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/Services/Auth/auth.service';
import { FormBuilder, Validators } from '@angular/forms'
import { Router } from '@angular/router';
import { OwlService } from 'src/app/Services/carousel/owl.service';
import { Response  } from 'src/app/Modules/Response/Response';
import { EmailService } from 'src/app/Services/Email/email.service';
import { EMAIL_CODE_VEREFICATION } from 'src/app/Modules/Config/Config';

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

  constructor(
    private formBuilder: FormBuilder,
    private AuthService: AuthService,
    private MailService: EmailService,
    private Owl: OwlService,
    private Router: Router
  ) {  }

  ngOnInit(): void {
    if(this.AuthService.Check())
    {
      this.Router.navigate(['/']);
    }
    this.Owl.owl();
    // Get User
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
        this.email = this.Validators.value.email;
        this.MailService.sendEmail(this.email, EMAIL_CODE_VEREFICATION.toString(), this.name);
        // this.Router.navigate(['/']);
        //To be Continued
      }
    }
  }

  ThrowError(Msg: string): void {
    this.message = Msg;
  }
}

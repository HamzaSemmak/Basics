import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/Services/Auth/auth.service';
import { Router } from '@angular/router';
import { EmailService } from 'src/app/Services/Email/email.service';

@Component({
  selector: 'app-forget-password',
  templateUrl: './forget-password.component.html',
  styleUrls: ['./forget-password.component.css']
})
export class ForgetPasswordComponent implements OnInit {

  constructor(
    private AuthService: AuthService,
    private EmailService: EmailService,
    private Router: Router
  ) {  }

  ngOnInit(): void {
    if(this.AuthService.Check())
    {
      this.Router.navigate(['/']);
    }
  }

  ngOnSubmit(): void {
    this.EmailService.sendEmail("hamzatest2001@gmail.com", "Test");
    console.log("g");
  }

}

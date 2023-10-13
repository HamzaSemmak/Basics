import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms'
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/Service/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  User: any;

  constructor(private builder: FormBuilder, private toastr: ToastrService, private Service: AuthService, private Route: Router) {
    this.Service.LoggOut();
  }

  loginForm = this.builder.group({
    name: this.builder.control('', Validators.required),
    password: this.builder.control('', Validators.required),
  })

  Login(): void {
    if (this.loginForm.valid) {
      this.Service.Find(this.loginForm.value.name, this.loginForm.value.password).subscribe(res => {
        this.User = res;
        if (Object.keys(this.User).length > 0) {
          if (this.User[0].isActive) {
            sessionStorage.setItem("User", this.User[0].id);
            this.Route.navigate([""]);
            this.toastr.success('User Logged Successfly!!')
          } else {
            this.toastr.warning('Conatct Administrateur for enabled Accout')
          }
        }
        else {
          this.toastr.error("User is Invalid");
        }
      })
    }
    else {
      this.toastr.warning('Please enter valid data');
    }
  }
}

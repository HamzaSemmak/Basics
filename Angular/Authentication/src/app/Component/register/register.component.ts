import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms'
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/Service/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})

export class RegisterComponent {
  constructor(private builder: FormBuilder, private toastr: ToastrService, private Service: AuthService, private Route: Router) {
    this.Service.LoggOut();
   }

  registerForm = this.builder.group({
    name: this.builder.control('', Validators.required),
    password: this.builder.control('', Validators.compose([Validators.required])),
    email: this.builder.control('', Validators.compose([Validators.required, Validators.email])),
    gender: this.builder.control('male'),
    role: this.builder.control(''),
    isActive: this.builder.control(false),
  })

  Register(): void {
    if(this.registerForm.valid) {
      this.Service.Create(this.registerForm.value).subscribe(Res => {
        this.toastr.success('Please Contact admin for enable access', 'Registered Successfly');
        this.Route.navigate(['login']);
      });
    }
    else {
      this.toastr.warning('Please enter valid data'); 
    }
  }
}

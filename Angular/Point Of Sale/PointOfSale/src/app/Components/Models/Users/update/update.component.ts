import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/Modules/Model/Users';
import { UserService } from 'src/app/Services/User/user.service';
import { FormBuilder, Validators } from '@angular/forms'
import { ActivatedRoute, Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { Response } from 'src/app/Modules/Error/Response';
import { ToastService } from 'src/app/Services/Toast/toast.service';
import { AuthService } from 'src/app/Services/Auth/auth.service';
import { elementAt } from 'rxjs';

@Component({
  selector: 'app-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.css']
})
export class UpdateComponent implements OnInit {
  User: User;
  Users: User;
  Key: string;
  error: boolean = true;
  message: string;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private userService: UserService,
    private toast: ToastService,
    private ActivateRouter: ActivatedRoute
  ) {  }

  ngOnInit(): void {
    this.Key = this.ActivateRouter.snapshot.params['key'];
    // this.userService.findByColumn('Key', this.Key).subscribe(
    //   (res) => this.Users = res[0]
    // )
  }

  Validators = this.formBuilder.group({
    name: this.formBuilder.control('', [Validators.required]),
    email: this.formBuilder.control('', [Validators.required, Validators.email]),
    password: this.formBuilder.control('', [Validators.required]),
    confirmPassword: this.formBuilder.control('', [Validators.required]),
    gender: this.formBuilder.control('', [Validators.required]),
    role: this.formBuilder.control('', [Validators.required]),
  });

  ngOnSubmit(): void {
    if(this.Validators.valid) {
      if(this.Validators.value.confirmPassword != this.Validators.value.password) {
        this.ThrowError(Response.RESPONSE_MSG_AUTH_PASSWORD_MATCH);
        return;
      }
      else {
        //
      }
    }
    else {
      this.ThrowError(Response.RESPONSE_MSG_VAILDATION_FORM);
      return;
    }
  }

  ConvertToUser(): User {
    var NewUser: any = {
      id: Math.floor(Math.random() * 100),
      name: this.Validators.value.name,
      gender: this.Validators.value.gender,
      email: this.Validators.value.email,
      password: this.Validators.value.password,
      Key: this.User.Key,
      role: this.Validators.value.role
    }
    this.User = NewUser;
    return this.User;
  }

  generateKey(length: number): string {
    let result = '';
    const characters = '-ABCDEFGHIJKLMNOPQRSTUVWXYZ-abcdefghijklmnopqrstuvwxyz-0123456789-';
    const charactersLength = characters.length;
    let counter = 0;
    while (counter < length) {
      result += characters.charAt(Math.floor(Math.random() * charactersLength));
      counter += 1;
    }
    return result;
  }

  ThrowError(Msg: string): void {
    this.message = Msg;
  }
}

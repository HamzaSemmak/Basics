import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms'
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/Service/auth.service';
import { Router } from '@angular/router';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog'

@Component({
  selector: 'app-update-pop-up',
  templateUrl: './update-pop-up.component.html',
  styleUrls: ['./update-pop-up.component.css']
})
export class UpdatePopUpComponent implements OnInit {
  roles: any;
  user: any;

  constructor (
    private builder: FormBuilder, private toastr: ToastrService, private Service: AuthService,
    private Route: Router, @Inject(MAT_DIALOG_DATA) public data: any,
    private MatDialogRed: MatDialogRef<UpdatePopUpComponent>
  ) {}

  ngOnInit(): void {
    this.Service.Roles().subscribe( res => {
      this.roles = res;
    })
    if(this.data.userID != null && this.data.userID != '')
    {
      this.Service.FindByID(this.data.userID).subscribe( res => {
        this.user = res;
        this.registerForm.setValue({
          id: this.user.id,
          name: this.user.name, 
          password: this.user.password,
          email: this.user.email,
          gender: this.user.gender,
          role: this.user.role,
          isActive: this.user.isActive,
        })
      })
    }
  }

  registerForm = this.builder.group({
    id: this.builder.control(''),
    name: this.builder.control(''),
    password: this.builder.control(''),
    email: this.builder.control(''),
    gender: this.builder.control(''),
    role: this.builder.control(''),
    isActive: this.builder.control(false),
  })

  Update(): void {
    if(this.registerForm.valid) {
      this.Service.Update(this.registerForm.value.id, this.registerForm.value).subscribe( res => {
        this.toastr.success('Updated Successfly');
        this.MatDialogRed.close();
      })
    }
    else {
      this.toastr.warning('Please Select Role'); 
    }
  }
}

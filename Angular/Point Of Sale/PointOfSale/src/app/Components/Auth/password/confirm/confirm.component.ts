import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute  } from '@angular/router';
import { AuthService } from 'src/app/Services/Auth/auth.service';

@Component({
  selector: 'app-confirm',
  templateUrl: './confirm.component.html',
  styleUrls: ['./confirm.component.css']
})
export class ConfirmComponent implements OnInit {
  Key: string;

  constructor(
    private Router: Router,
    private ActivateRoute: ActivatedRoute,
    private Service: AuthService
  ) {  }

  ngOnInit(): void {
    this.Key = this.ActivateRoute.snapshot.params['key'];
    this.Service.FindByColumn("Key", this.Key).subscribe(
      Response => {
        if(Object.keys(Response).length > 0)
        {
          return;
        }
        else {
          this.Router.navigate(['/auth/login']);
        }
      }
    )
  }

  ngOnSubmit(): void {
    this.Router.navigate(['/auth/login']);
  }
}

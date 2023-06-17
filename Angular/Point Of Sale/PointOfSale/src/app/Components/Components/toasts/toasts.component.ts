import { Component, OnInit  } from '@angular/core';
import { ToastService } from 'src/app/Services/Toast/toast.service';

@Component({
  selector: 'app-toasts',
  templateUrl: './toasts.component.html',
  styleUrls: ['./toasts.component.css']
})
export class ToastsComponent implements OnInit {

  constructor(private ToastService: ToastService) { }

  ngOnInit(): void {
    return;
  }

  ngOnClose(): void {
    this.ToastService.Close();
  }
}

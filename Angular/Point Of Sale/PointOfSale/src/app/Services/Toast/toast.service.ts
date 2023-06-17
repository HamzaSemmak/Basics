import { Injectable } from '@angular/core';
import { ToastsLevels as level } from 'src/app/Modules/Toast/ToastLevel';

@Injectable({
  providedIn: 'root'
})

export class ToastService {
  Alert: HTMLAnchorElement | null;
  Msg: HTMLElement
  constructor() { }

  success(message: string): void {
    this.toast(message, level.Success);
  }

  error(message: string): void {
    this.toast(message, level.Error);
  }

  info(message: string): void {
    this.toast(message, level.Info);
  }

  warning(message: string): void {
    this.toast(message, level.Warning);
  }

  toast(message: string, level: level): void {
    this.Alert = document.querySelector(level);
    this.Alert?.classList.add('active');
    this.Msg = document.querySelector(`${level} .alert-text-content`) as HTMLElement
    this.Msg.innerHTML = message;
    setTimeout(() => {
      this.Alert?.classList.remove('active');
    }, 9000);
  }

  Close(): void {
    var alert = document.querySelectorAll('.alert');
    alert.forEach(element => {
      element.classList.remove('active');
    });
    //Hamza Semmak
  }
}

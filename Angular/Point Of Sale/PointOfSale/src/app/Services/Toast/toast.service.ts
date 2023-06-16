import { Injectable } from '@angular/core';
import { ToastsLevels as level } from 'src/app/Modules/Toast/ToastLevel';

@Injectable({
  providedIn: 'root'
})

export class ToastService {
  Alert: HTMLAnchorElement | null;

  constructor() { }

  success(message: string): void {
    this.toast(message, level.Success);
  }

  error(message: string): void {
    this.toast(message, level.Success);
  }

  info(message: string): void {
    this.toast(message, level.Success);
  }

  warning(message: string): void {
    this.toast(message, level.Success);
  }

  toast(message: string, level: level): void {
    this.Alert = document.querySelector(level);
    this.Alert?.classList.add('active');
    console.log(message);
  }
}

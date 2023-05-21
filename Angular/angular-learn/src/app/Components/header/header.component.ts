import { Component } from '@angular/core';
import { UiService  } from 'src/app/services/ui.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
  title = 'Task Tracker';
  showAddTsk: boolean = true;
  subscription: Subscription;

  constructor(private uiService: UiService) {
    this.subscription = this.uiService.onToggle().subscribe((Val) => this.showAddTsk = Val);
  }

  ngOnInit(): void { return; }

  toggleAddTask(data: any): void { 
    this.uiService.toggleAddTask();
  }
}

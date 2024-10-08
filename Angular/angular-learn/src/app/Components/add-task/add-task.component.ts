import { Component, Output, EventEmitter } from '@angular/core';
import { Task } from '../../Task';
import { UiService  } from 'src/app/services/ui.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-add-task',
  templateUrl: './add-task.component.html',
  styleUrls: ['./add-task.component.css']
})

export class AddTaskComponent {
  @Output() onAddTask: EventEmitter<Task> = new EventEmitter();
  text: string;
  day: string;
  reminder: boolean = false
  showAddTsk: boolean = true;
  subscription: Subscription;

  constructor(private uiService: UiService) {
    this.subscription = this.uiService.onToggle().subscribe((Val) => this.showAddTsk = Val);
  }

  onSubmit(): void {
    if(!this.text || !this.day)
    {
      alert('There is an empty field !!!');
      return;
    }
    const newTask = {
      text: this.text,
      day: this.day,
      reminder: this.reminder
    }

    this.onAddTask.emit(newTask);

    this.text = ''
    this.day = ''
    this.reminder = false
  }

}

import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-notification',
  imports: [CommonModule],
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css']
})
export class NotificationComponent {
  visible = false;
  message = '';
  type: 'success' | 'error' | 'warn' = 'success';

  show(message: string, type: 'success' | 'error' | 'warn' = 'success') {
    this.message = message;
    this.type = type;
    this.visible = true;

    setTimeout(() => {
      this.visible = false;
    }, 5000);
  }
}

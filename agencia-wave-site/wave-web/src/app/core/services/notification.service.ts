import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class NotificationService {
  private componentRef!: any;

  register(componentRef: any) {
    this.componentRef = componentRef;
  }

  show(message: string, type: 'success' | 'error' = 'success') {
    this.componentRef?.show(message, type);
  }
}
import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from './components/navbar/navbar.component';
import { FooterComponent } from './components/footer/footer.component';
import { RouteCheckerService } from './core/services/route-checker.service';
import { CommonModule } from '@angular/common';
import { NotificationComponent } from '../app/components/notification/notification.component';
import { NotificationService } from '../app/core/services/notification.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    NavbarComponent,
    FooterComponent,
    CommonModule,
    NotificationComponent
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements AfterViewInit{
  @ViewChild(NotificationComponent) notificationComponent!: NotificationComponent;

  constructor(public routeChecker: RouteCheckerService, private notificationService: NotificationService) { }
  
  ngAfterViewInit(): void {
    this.notificationService.register(this.notificationComponent);
  }
}

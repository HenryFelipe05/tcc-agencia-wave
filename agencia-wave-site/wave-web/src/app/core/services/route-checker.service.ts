import { Injectable } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class RouteCheckerService {
  private isLoginPage = false;
  private isCreateAccountPage = false;

  constructor(private router: Router) {
    this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe((event: NavigationEnd) => {
        this.isLoginPage = event.url == '/login';
        this.isCreateAccountPage = event.url === '/criar-conta';
      });
  }

  isLoginRoute(): boolean {
    return this.isLoginPage;
  }

  isCreateAccountRoute(): boolean {
    return this.isCreateAccountPage;
  }
}

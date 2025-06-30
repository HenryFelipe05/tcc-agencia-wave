import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-navbar-mobile',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './navbar-mobile.component.html',
  styleUrl: './navbar-mobile.component.css'
})
export class NavbarMobileComponent {
  @Output() navItemClicked = new EventEmitter<void>();

  constructor(
    public authService: AuthService,
    private router: Router
  ) { }

  onNavItemClick() {
    this.navItemClicked.emit();
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/']);
  }

  get isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }
}

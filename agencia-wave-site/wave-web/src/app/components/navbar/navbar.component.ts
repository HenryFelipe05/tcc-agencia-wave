import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { NavbarMobileComponent } from '../../components/navbar-mobile/navbar-mobile.component';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterModule, NavbarMobileComponent],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  isDropdownVisible = false;
  isMobileNavVisible = false;

  constructor(
    public authService: AuthService,
    private router: Router
  ) {}

  toggleDropdown() {
    this.isDropdownVisible = !this.isDropdownVisible;
  }

  toggleMobileNav() {
    this.isMobileNavVisible = !this.isMobileNavVisible;
    document.body.style.overflow = this.isMobileNavVisible ? 'hidden' : '';
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/']);
  }

  get isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }
}

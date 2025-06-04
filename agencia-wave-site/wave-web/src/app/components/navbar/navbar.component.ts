import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { NavbarMobileComponent } from '../../components/navbar-mobile/navbar-mobile.component';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterModule, NavbarMobileComponent],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  isDropdownVisible: boolean = false;
  isMobileNavVisible: boolean = false;

  toggleDropdown() {
    this.isDropdownVisible = !this.isDropdownVisible;
  }

  toggleMobileNav() {
    this.isMobileNavVisible = !this.isMobileNavVisible;

    if (this.isMobileNavVisible) {
      document.body.style.overflow = 'hidden'; 
    } else {
      document.body.style.overflow = '';
    }
  }
}

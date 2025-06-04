import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-navbar-mobile',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './navbar-mobile.component.html',
  styleUrl: './navbar-mobile.component.css'
})
export class NavbarMobileComponent {
  @Output() navItemClicked = new EventEmitter<void>();

  onNavItemClick() {
    this.navItemClicked.emit();
  }
}

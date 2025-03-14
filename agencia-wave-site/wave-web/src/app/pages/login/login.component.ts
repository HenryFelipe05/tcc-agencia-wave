import { Component } from '@angular/core';
import { TitleService } from '../../core/services/title.service';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  constructor(
    private titleService: TitleService,
    private router: Router
  ) {
    this.titleService.updateTitle('Login');
  }

  onLoginSubmit(): void {
    this.router.navigate(['/']); 
  }
}

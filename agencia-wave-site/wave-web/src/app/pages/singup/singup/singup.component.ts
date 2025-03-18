import { Component } from '@angular/core';
import { TitleService } from '../../../core/services/title.service';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-singup',
  imports: [CommonModule, RouterModule],
  templateUrl: './singup.component.html',
  styleUrl: './singup.component.css'
})
export class SingupComponent {
  constructor(
    private titleService: TitleService,
    private router: Router
  ) {
    this.titleService.updateTitle('Criar Conta');
  }

  onCreateAcountSubmit(): void {
    this.router.navigate(['/']); 
  }
}

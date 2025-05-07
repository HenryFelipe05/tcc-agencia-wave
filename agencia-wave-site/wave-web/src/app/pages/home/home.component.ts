import { Component } from '@angular/core';
import { TitleService } from '../../core/services/title.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { NgIcon, provideIcons } from '@ng-icons/core';
import { bootstrapInstagram, bootstrapLinkedin } from '@ng-icons/bootstrap-icons';

@Component({
  selector: 'app-home',
  standalone: true, 
  imports: [CommonModule, RouterModule, NgIcon],
  viewProviders: [provideIcons({ bootstrapInstagram, bootstrapLinkedin })],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  /* Mostra o nome da página no title */
  constructor(private titleService: TitleService) {
    this.titleService.updateTitle('');
  }
}

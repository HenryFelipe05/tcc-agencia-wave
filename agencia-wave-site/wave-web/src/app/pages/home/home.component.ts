import { Component } from '@angular/core';
import { TitleService } from '../../core/services/title.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true, 
  imports: [CommonModule, RouterModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  /* Mostra o nome da página no title */
  constructor(private titleService: TitleService) {
    this.titleService.updateTitle('');
  }
}

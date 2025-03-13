import { Component } from '@angular/core';
import { TitleService } from '../../core/services/title.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-contact',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './contact.component.html',
  styleUrl: './contact.component.css'
})
export class ContactComponent {
  /* Mostra o nome da página no title */
  constructor(private titleService: TitleService) {
    this.titleService.updateTitle('Contato');
  }
}


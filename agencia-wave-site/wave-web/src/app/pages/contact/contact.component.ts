import { Component } from '@angular/core';
import { TitleService } from '../../core/services/title.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgxMaskDirective, provideNgxMask } from 'ngx-mask';

@Component({
  selector: 'app-contact',
  standalone: true,
  imports: [CommonModule, FormsModule, NgxMaskDirective],
  providers: [provideNgxMask()],
  templateUrl: './contact.component.html',
  styleUrl: './contact.component.css'
})
export class ContactComponent {
  telefone: any = '';

  /* Mostra o nome da página no title */
  constructor(private titleService: TitleService) {
    this.titleService.updateTitle('Contato');
  }
}


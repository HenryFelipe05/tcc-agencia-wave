import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { NgxMaskDirective, provideNgxMask } from 'ngx-mask';
import { TitleService } from '../../core/services/title.service';

@Component({
  selector: 'app-user-details',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule, NgxMaskDirective],
  providers: [provideNgxMask()],
  templateUrl: './user-details.component.html',
  styleUrl: './user-details.component.css'
})
export class UserDetailsComponent {
  telefone: any = '';
  data: any = '';

  constructor(private titleService: TitleService) {
    this.titleService.updateTitle('Detalhes do Usuário');
  }
}

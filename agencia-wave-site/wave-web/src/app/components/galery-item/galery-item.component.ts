import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { environment } from '../../../environment';

@Component({
  selector: 'app-galery-item',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './galery-item.component.html',
  styleUrl: './galery-item.component.css'
})
export class GaleryItemComponent {
  @Input() titulo: string = '';
  @Input() descricao: string = '';
  @Input() dataPublicacao?: string;
  @Input() arquivo: string = '';
  @Input() extensaoArquivo: string = '';

  get imagemUrl(): string {
    return `${environment.baseUrl}/uploads/${this.arquivo}`;
  }
}

import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgIcon, provideIcons } from '@ng-icons/core';
import { bootstrapDownload } from '@ng-icons/bootstrap-icons';
import { GaleriaService } from '../../services/galeria-service/galeria.service';
import { NotificationService } from '../../core/services/notification.service';

@Component({
  selector: 'app-galery-item',
  standalone: true,
  imports: [CommonModule, NgIcon],
  viewProviders: [provideIcons({ bootstrapDownload })],
  templateUrl: './galery-item.component.html',
  styleUrl: './galery-item.component.css'
})
export class GaleryItemComponent {
  @Input() titulo: string = '';
  @Input() descricao: string = '';
  @Input() urlMiniatura?: string;
  @Input() dataCadastro?: any;
  @Input() arquivo!: string;
  @Input() extensaoArquivo: string = '';
  @Input() codigoItemGaleria!: number;

  constructor(private galeriaService: GaleriaService, private notificationService: NotificationService) { }

  baixar(): void {
    if (!this.codigoItemGaleria) {
      console.warn('codigoItemGaleria está indefinido.');
      return;
    }

    this.galeriaService.baixarArquivoPorCodigo(this.codigoItemGaleria).subscribe({
      next: (blob) => {
        const url = window.URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url;
        a.download = `${this.titulo || 'arquivo'}-${this.codigoItemGaleria}.${this.extensaoArquivo}`;
        a.click();
        window.URL.revokeObjectURL(url);
      },
      error: (err) => {
        console.error('Erro ao baixar arquivo:', err);
        this.notificationService.show(`Erro ao baixar arquivo: ${err.message}`, 'error');
      }
    });
  }
}

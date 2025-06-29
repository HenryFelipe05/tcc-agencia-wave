import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { TitleService } from '../../core/services/title.service';
import { GaleryItemComponent } from '../../components/galery-item/galery-item.component';
import { GaleryNavComponent } from '../../components/galery-nav/galery-nav.component';
import { GaleriaService } from '../../services/galeria-service/galeria.service';

@Component({
  selector: 'app-galery',
  standalone: true,
  imports: [CommonModule, GaleryItemComponent, GaleryNavComponent],
  templateUrl: './galery.component.html',
  styleUrl: './galery.component.css'
})
export class GaleryComponent {
  constructor(private titleService: TitleService, private galeriaService: GaleriaService) {
    this.titleService.updateTitle('Galeria');
  }

  ngOnInit(): void {
    this.galeriaService.recuperarItensGaleria().subscribe({
      next: (itens) => {
        console.log('Itens da galeria:', itens);
      },
      error: (err) => {
        console.error('Erro ao carregar galeria:', err);
      }
    });
  }
}

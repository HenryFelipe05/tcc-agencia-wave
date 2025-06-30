import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { TitleService } from '../../core/services/title.service';
import { GaleryItemComponent } from '../../components/galery-item/galery-item.component';
import { GaleriaService } from '../../services/galeria-service/galeria.service';

@Component({
  selector: 'app-galery',
  standalone: true,
  imports: [CommonModule, GaleryItemComponent],
  templateUrl: './galery.component.html',
  styleUrl: './galery.component.css'
})
export class GaleryComponent implements OnInit {
  itensGaleria: any[] = [];

  constructor(
    private titleService: TitleService,
    private galeriaService: GaleriaService
  ) {
    this.titleService.updateTitle('Galeria');
  }

  ngOnInit(): void {
    this.galeriaService.recuperarItensGaleria().subscribe({
      next: (itens) => {
        this.itensGaleria = itens;
        console.log('Itens da galeria:', itens);
      },
      error: (err) => {
        console.error('Erro ao carregar galeria:', err);
      }
    });
  }
}

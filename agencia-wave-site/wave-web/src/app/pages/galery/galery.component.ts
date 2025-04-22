import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { TitleService } from '../../core/services/title.service';
import { GaleryItemComponent } from '../../components/galery-item/galery-item.component';

@Component({
  selector: 'app-galery',
  standalone: true,
  imports: [CommonModule, GaleryItemComponent],
  templateUrl: './galery.component.html',
  styleUrl: './galery.component.css'
})
export class GaleryComponent {
  constructor(private titleService: TitleService) {
    this.titleService.updateTitle('Galeria');
  }
}

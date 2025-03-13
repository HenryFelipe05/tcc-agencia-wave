import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { TitleService } from '../../core/services/title.service';

@Component({
  selector: 'app-subscription',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './subscription.component.html',
  styleUrl: './subscription.component.css'
})
export class SubscriptionComponent {
  constructor(private titleService: TitleService) {
    this.titleService.updateTitle('Assinaturas');
  }
}

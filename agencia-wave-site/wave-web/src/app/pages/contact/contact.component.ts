import { AfterViewInit, Component, ElementRef, ViewChild } from '@angular/core';
import { TitleService } from '../../core/services/title.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgxMaskDirective, provideNgxMask } from 'ngx-mask';
import { NotificationService } from '../../core/services/notification.service';

@Component({
  selector: 'app-contact',
  standalone: true,
  imports: [CommonModule, FormsModule, NgxMaskDirective],
  providers: [provideNgxMask()],
  templateUrl: './contact.component.html',
  styleUrl: './contact.component.css'
})
export class ContactComponent implements AfterViewInit {
  telefone: any = '';

  @ViewChild('formContact') form!: ElementRef<HTMLFormElement>;

  constructor(private titleService: TitleService, private notificationService: NotificationService) {
    this.titleService.updateTitle('Contato');
  }

  ngAfterViewInit(): void {
    const formEl = this.form.nativeElement;

    formEl.addEventListener('submit', (event) => {
      event.preventDefault();

      if (!formEl.checkValidity()) {
        formEl.reportValidity();
        return;
      }
      this.notificationService.show('Mensagem enviada com sucesso! Em breve entraremos em contato.', 'success');

      setTimeout(() => {
        formEl.reset();
        this.telefone = '';
      }, 100);
    });
  }
}
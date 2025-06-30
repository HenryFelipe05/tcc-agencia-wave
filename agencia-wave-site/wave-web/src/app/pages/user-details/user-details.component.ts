import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { NgxMaskDirective, provideNgxMask } from 'ngx-mask';
import { TitleService } from '../../core/services/title.service';
import { UsuarioService } from '../../services/usuario-service/usuario-service.service';
import { NotificationService } from '../../core/services/notification.service';

@Component({
  selector: 'app-user-details',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule, NgxMaskDirective],
  providers: [provideNgxMask()],
  templateUrl: './user-details.component.html',
  styleUrl: './user-details.component.css'
})
export class UserDetailsComponent implements OnInit {
  usuario: any = {};

  constructor(
    private titleService: TitleService,
    private usuarioService: UsuarioService,
    private notificationService: NotificationService
  ) {
    this.titleService.updateTitle('Detalhes do Usuário');
  }

  ngOnInit(): void {
    this.usuarioService.recuperarDadosUsuario().subscribe({
      next: (res) => {
        console.log('Dados do usuário logado:', res);
        this.usuario = res;
        this.usuario.dataNascimento = this.formatarData(res.dataNascimento);
      },
      error: (err) => {
        this.notificationService.show(err.error || 'Erro ao recuperar dados do usuário.', 'error');
      }
    });
  }

  private formatarData(dataISO: string): string {
    const data = new Date(dataISO);
    const dia = String(data.getDate()).padStart(2, '0');
    const mes = String(data.getMonth() + 1).padStart(2, '0');
    const ano = data.getFullYear();
    return `${dia}/${mes}/${ano}`;
  }

}

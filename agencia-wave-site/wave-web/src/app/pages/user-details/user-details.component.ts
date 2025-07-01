import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
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
    private router: Router,
    private titleService: TitleService,
    private usuarioService: UsuarioService,
    private notificationService: NotificationService
  ) {
    this.titleService.updateTitle('Detalhes do Usuário');
  }

  ngOnInit(): void {
    this.usuarioService.recuperarDadosUsuario().subscribe({
      next: (res) => {
        this.usuario = res;
        this.usuario.dataNascimento = res.dataNascimento
          ? this.formatarData(res.dataNascimento)
          : '';

        this.usuario.telefone = res.telefone;
        this.usuario.nome = `${res.nome} ${res.sobrenome}`;
      },
      error: (err) => {
        this.notificationService.show(err.error || 'Erro ao recuperar dados do usuário.', 'error');
      }
    });
  }

  salvar(): void {
    let dataNascimentoStr = this.usuario.dataNascimento;

    if (/^\d{8}$/.test(dataNascimentoStr)) {
      dataNascimentoStr = dataNascimentoStr.replace(/(\d{2})(\d{2})(\d{4})/, '$1/$2/$3');
    }

    if (!/^\d{2}\/\d{2}\/\d{4}$/.test(dataNascimentoStr)) {
      this.notificationService.show('Data de nascimento inválida.', 'error');
      return;
    }

    const partes = dataNascimentoStr.split('/');
    const dia = parseInt(partes[0], 10);
    const mes = parseInt(partes[1], 10) - 1;
    const ano = parseInt(partes[2], 10);

    const dataNascimento = new Date(ano, mes, dia);
    if (isNaN(dataNascimento.getTime())) {
      this.notificationService.show('Data de nascimento inválida.', 'error');
      return;
    }

    const dataNascimentoISO = dataNascimento.toISOString();

    const nomeCompleto = this.usuario.nome?.trim().split(' ');
    const nome = nomeCompleto?.[0] || '';
    const sobrenome = nomeCompleto?.slice(1).join(' ') || '';

    const dadosAtualizados = {
      email: this.usuario.email,
      telefone: this.usuario.telefone,
      nome,
      sobrenome,
      dataNascimento: dataNascimentoISO
    };

    this.usuarioService.alterarUsuario(dadosAtualizados).subscribe({
      next: (res) => {
        this.notificationService.show(res?.message || 'Dados atualizados com sucesso!', 'success');
        this.router.navigate(['/login']);
      },
      error: (err) => {
        this.notificationService.show('Dados atualizados com sucesso!', 'success');
        this.router.navigate(['/']);
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

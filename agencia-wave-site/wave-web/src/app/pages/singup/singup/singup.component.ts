import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UsuarioService } from '../../../services/usuario-service/usuario-service.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { NotificationService } from '../../../core/services/notification.service';
import { NgxMaskDirective, provideNgxMask } from 'ngx-mask';

@Component({
  selector: 'app-singup',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule, NgxMaskDirective],
  providers: [provideNgxMask()],
  templateUrl: './singup.component.html',
  styleUrl: './singup.component.css'
})
export class SingupComponent {
  telefone: any = '';
  dataNascimento: any = '';

  constructor(
    private router: Router,
    private usuarioService: UsuarioService,
    private notificationService: NotificationService
  ) { }

  onCreateAcountSubmit(event: Event): void {
    const form = document.getElementById('formSingup') as HTMLFormElement;

    if (!form.checkValidity()) {
      form.reportValidity();
      return;
    }

    event.preventDefault();

    const nomeCompleto = (document.getElementById('inputName') as HTMLInputElement).value;
    const email = (document.getElementById('inputEmail') as HTMLInputElement).value;
    const senha = (document.getElementById('inputPassword') as HTMLInputElement).value;
    const confirmarSenha = (document.getElementById('inputConfirmPassword') as HTMLInputElement).value;
    const telefone = (document.getElementById('inputPhone') as HTMLInputElement).value;
    const dataNascimentoStr = (document.getElementById('inputData') as HTMLInputElement).value;

    const partes = dataNascimentoStr.split('/');
    const dia = parseInt(partes[0], 10);
    const mes = parseInt(partes[1], 10) - 1; 
    const ano = parseInt(partes[2], 10);

    const dataNascimento = new Date(ano, mes, dia);
    const dataNascimentoISO = dataNascimento.toISOString();

    const nome = nomeCompleto.split(' ')[0];
    const sobrenome = nomeCompleto.split(' ').slice(1).join(' ');

    const dadosCadastro = {
      nome: nome,
      sobrenome: sobrenome,
      dataNascimento: dataNascimentoISO,
      codigoGenero: 1,
      nomeUsuario: email,
      email: email,
      telefone: telefone,
      senha: senha,
      senhaConfirmada: confirmarSenha,
      ativo: true,
      codigoPerfil: 1
    };

    this.usuarioService.registrarUsuario(dadosCadastro).subscribe({
      next: (res) => {
        this.notificationService.show('Cadastro realizado com sucesso!', 'success');
        this.router.navigate(['/login']);
      },
      error: (err) => {
        console.error('Erro ao registrar usuário:', err);
        this.notificationService.show(err.error || 'Erro ao registrar. Tente novamente.', 'error');
      }
    });
  }
}


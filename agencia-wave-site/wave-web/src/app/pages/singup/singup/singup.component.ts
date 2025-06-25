import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UsuarioService } from '../../../services/usuario-service/usuario-service.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-singup',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './singup.component.html',
  styleUrl: './singup.component.css'
})
export class SingupComponent {
  constructor(
    private router: Router,
    private usuarioService: UsuarioService
  ) {}

  onCreateAcountSubmit(event: Event): void {
    event.preventDefault();
    console.log('Submit chamado');

    const nomeCompleto = (document.getElementById('inputName') as HTMLInputElement).value;
    const email = (document.getElementById('inputEmail') as HTMLInputElement).value;
    const senha = (document.getElementById('inputPassword') as HTMLInputElement).value;
    const confirmarSenha = (document.getElementById('inputConfirmPassword') as HTMLInputElement).value;

    const nome = nomeCompleto.split(' ')[0];
    const sobrenome = nomeCompleto.split(' ').slice(1).join(' ');

    const dadosCadastro = {
      nome: nome,
      sobrenome: sobrenome,
      dataNascimento: '2000-01-01',
      codigoGenero: 1,
      nomeUsuario: email,
      email: email,
      telefone: '',
      senha: senha,
      senhaConfirmada: confirmarSenha,
      ativo: true,
      codigoPerfil: 1
    };

    this.usuarioService.registrarUsuario(dadosCadastro).subscribe({
      next: (res) => {
        alert('Cadastro realizado com sucesso!');
        this.router.navigate(['/login']); 
      },
      error: (err) => {
        console.error('Erro ao registrar usuário:', err);
        alert(err.error || 'Erro ao registrar. Tente novamente.');
      }
    });
  }
}
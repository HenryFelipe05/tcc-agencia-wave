import { Component } from '@angular/core';
import { TitleService } from '../../core/services/title.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UsuarioService } from '../../services/usuario-service/usuario-service.service';
import { RouterModule, Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  constructor(
    private titleService: TitleService,
    private router: Router,
    private usuarioService: UsuarioService
  ) {
    this.titleService.updateTitle('Login');
  }

  onLoginSubmit(event: Event): void {
    const form = document.getElementById('formLogin') as HTMLFormElement;

    if (!form.checkValidity()) {
      form.reportValidity(); 
      return;
    }

    event.preventDefault();

    const email = (document.getElementById('inputEmail') as HTMLInputElement).value;
    const senha = (document.getElementById('inputPassword') as HTMLInputElement).value;

    const dadosAutenticacao = {
      username: email,
      password: senha
    };

    this.usuarioService.autenticarUsuario(dadosAutenticacao).subscribe({
      next: (res) => {
        this.router.navigate(['/']); 
      },
      error: (err) => {
        console.error('Erro ao autenticar usuário:', err);
        alert(err.error || 'Erro ao autenticar. Tente novamente.');
      }
    });
  }
}

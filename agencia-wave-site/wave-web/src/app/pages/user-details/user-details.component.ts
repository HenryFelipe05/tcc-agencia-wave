import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { NgxMaskDirective, provideNgxMask } from 'ngx-mask';
import { TitleService } from '../../core/services/title.service';
import { HttpClient, HttpHeaders   } from '@angular/common/http';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-user-details',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule, NgxMaskDirective],
  providers: [provideNgxMask()],
  templateUrl: './user-details.component.html',
  styleUrl: './user-details.component.css'
})
export class UserDetailsComponent implements OnInit {
  usuario = {
    nome: '',
    email: '',
    telefone: '',
    dataNascimento: ''
  };

  constructor(
    private titleService: TitleService,
    private http: HttpClient,
    private authService: AuthService
  ) {
    this.titleService.updateTitle('Detalhes do Usuário');
  }

ngOnInit(): void {
  const token = this.authService.getToken();

  if (!token) {
    console.warn('Token não encontrado. Usuário não autenticado.');
    return;
  }

  const headers = new HttpHeaders({
    'Authorization': `Bearer ${token}`
  });

  this.http.get<any>('https://localhost:7261/Usuario/Recuperar-Dados', { headers }).subscribe({
    next: (res) => {
      console.log('Resposta da API:', res);
      this.usuario.nome = res.nome;
      this.usuario.email = res.email;
      this.usuario.telefone = res.telefone;
      this.usuario.dataNascimento = res.dataNascimento?.substring(0, 10);
      console.log('Usuário carregado:', this.usuario);
    },
    error: (err) => {
      console.error('Erro ao carregar dados do usuário:', err);
    }
  });
}


  salvar(): void {
    console.log('Dados salvos:', this.usuario);
    // Aqui você pode enviar os dados com um PUT/PATCH
  }
}

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environment';
import { AuthService } from '../../core/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {
  constructor(private http: HttpClient, private authService: AuthService) { }

  registrarUsuario(dados: any): Observable<any> {
    return this.http.post<any>(`${environment.baseUrl}/Autenticacao/registrarUsuario/Pessoa`, dados);
  }

  autenticarUsuario(dados: any): Observable<any> {
    return this.http.post<any>(`${environment.baseUrl}/Autenticacao/login`, dados);
  }

  recuperarDadosUsuario(): Observable<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.authService.getToken()}`
    });

    return this.http.get<any>(`${environment.baseUrl}/Usuario/Recuperar-Dados`, { headers });
  }

  alterarUsuario(dados: any): Observable<any> {
    return this.http.post<any>(`${environment.baseUrl}/Autenticacao/login`, dados);
  }
}

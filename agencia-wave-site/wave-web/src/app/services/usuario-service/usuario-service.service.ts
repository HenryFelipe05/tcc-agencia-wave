import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environment';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {
  constructor(private http: HttpClient) {}

  registrarUsuario(dados: any): Observable<any> {
    return this.http.post<any>(`${environment.baseUrl}/Autenticacao/registrarUsuario/Pessoa`, dados);
  }

  autenticarUsuario(dados: any): Observable<any> {
    return this.http.post<any>(`${environment.baseUrl}/Autenticacao/login`, dados);
  }
}

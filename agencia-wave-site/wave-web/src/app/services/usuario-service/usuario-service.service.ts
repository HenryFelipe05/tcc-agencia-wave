import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {
  private apiUrl = 'https://localhost:44314/api/Autenticacao/registrarUsuario/Pessoa';

  constructor(private http: HttpClient) {}

  registrarUsuario(dados: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, dados);
  }
}

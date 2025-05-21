import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GaleriaService {
  private apiUrl = `${environment.baseUrl}/Galeria`;

  constructor(private http: HttpClient) { }

  criarItem(command: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/Criar`, command);
  }

  baixarItem(codigoItemGaleria: number): Observable<Blob> {
    return this.http.get(`${this.apiUrl}/baixar/${codigoItemGaleria}`, {
      responseType: 'blob'
    });
  }

  buscarItens(queryParams: any): Observable<any[]> {
    let params = new HttpParams();
    for (let key in queryParams) {
      if (queryParams[key] !== null && queryParams[key] !== undefined) {
        params = params.set(key, queryParams[key]);
      }
    }
    return this.http.get<any[]>(`${this.apiUrl}/Filtrar`, { params });
  }

  alterarItem(command: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/Alterar Item`, command);
  }

  excluirItem(codigoItemGaleria: number, codigoUsuario: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/Excluir Item`, {
      params: {
        codigoItemGaleria: codigoItemGaleria.toString(),
        codigoUsuario: codigoUsuario.toString()
      }
    });
  }
}

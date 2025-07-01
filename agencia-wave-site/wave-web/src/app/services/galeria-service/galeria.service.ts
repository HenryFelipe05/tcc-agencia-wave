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

  recuperarItensGaleria(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/recuperar-itens`);
  }

  baixarArquivoPorCodigo(codigoItemGaleria: number): Observable<Blob> {
    return this.http.get(`${this.apiUrl}/baixar/${codigoItemGaleria}`, {
      responseType: 'blob'
    });
  }
}

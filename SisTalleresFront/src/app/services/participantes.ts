import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Participante } from '../models/participante.model';

@Injectable({
  providedIn: 'root'
})
export class Participantes {
  private apiUrl = 'http://localhost:5236/api/participantes';

  constructor(private http: HttpClient) {}

  getParticipantes(): Observable<Participante[]> {
    return this.http.get<Participante[]>(this.apiUrl);
  }

  getParticipante(id: number): Observable<Participante> {
    return this.http.get<Participante>(`${this.apiUrl}/${id}`);
  }

  crearParticipante(participante: Participante): Observable<Participante> {
    return this.http.post<Participante>(this.apiUrl, participante);
  }

  actualizarParticipante(id: number, participante: Participante): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, participante);
  }

  eliminarParticipante(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
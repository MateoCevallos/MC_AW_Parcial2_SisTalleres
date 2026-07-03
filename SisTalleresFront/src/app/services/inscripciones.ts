import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Inscripcion, NuevaInscripcion } from '../models/inscripcion.model';

export interface ActualizarInscripcion {
  inscripcionId: number;
  tallerId: number;
  participanteId: number;
  fechaInscripcion: string;
  estado: string;
}

@Injectable({
  providedIn: 'root'
})
export class Inscripciones {
  private apiUrl = 'http://localhost:5236/api/inscripciones';

  constructor(private http: HttpClient) {}

  getInscripciones(): Observable<Inscripcion[]> {
    return this.http.get<Inscripcion[]>(this.apiUrl);
  }

  getInscripcion(id: number): Observable<Inscripcion> {
    return this.http.get<Inscripcion>(`${this.apiUrl}/${id}`);
  }

  crearInscripcion(inscripcion: NuevaInscripcion): Observable<any> {
    return this.http.post<any>(this.apiUrl, inscripcion);
  }

  actualizarInscripcion(id: number, data: ActualizarInscripcion): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, data);
  }

  eliminarInscripcion(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
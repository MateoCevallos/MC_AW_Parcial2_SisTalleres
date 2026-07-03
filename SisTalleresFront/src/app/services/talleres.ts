import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Taller } from '../models/taller.model';

@Injectable({
  providedIn: 'root'
})
export class Talleres {
  private apiUrl = 'http://localhost:5236/api/talleres';

  constructor(private http: HttpClient) {}

  getTalleres(): Observable<Taller[]> {
    return this.http.get<Taller[]>(this.apiUrl);
  }

  getTaller(id: number): Observable<Taller> {
    return this.http.get<Taller>(`${this.apiUrl}/${id}`);
  }

  crearTaller(taller: Taller): Observable<Taller> {
    return this.http.post<Taller>(this.apiUrl, taller);
  }

  actualizarTaller(id: number, taller: Taller): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, taller);
  }

  eliminarTaller(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
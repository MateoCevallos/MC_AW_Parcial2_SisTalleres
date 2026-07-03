import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Inscripciones as InscripcionesService, ActualizarInscripcion } from '../../services/inscripciones';
import { Talleres as TalleresService } from '../../services/talleres';
import { Participantes as ParticipantesService } from '../../services/participantes';
import { Inscripcion, NuevaInscripcion } from '../../models/inscripcion.model';
import { Taller } from '../../models/taller.model';
import { Participante } from '../../models/participante.model';

@Component({
  selector: 'app-inscripciones',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './inscripciones.html',
  styleUrl: './inscripciones.css'
})
export class Inscripciones implements OnInit {
  inscripciones: Inscripcion[] = [];
  talleres: Taller[] = [];
  participantes: Participante[] = [];

  nuevaInscripcion: NuevaInscripcion = { tallerId: 0, participanteId: 0 };
  mensajeError = '';
  mensajeExito = '';

  constructor(
    private inscripcionesService: InscripcionesService,
    private talleresService: TalleresService,
    private participantesService: ParticipantesService
  ) {}

  ngOnInit(): void {
    this.cargarInscripciones();
    this.cargarTalleres();
    this.cargarParticipantes();
  }

  cargarInscripciones(): void {
    this.inscripcionesService.getInscripciones().subscribe({
      next: (data) => (this.inscripciones = data),
      error: (err) => (this.mensajeError = 'Error al cargar las inscripciones: ' + err.message)
    });
  }

  cargarTalleres(): void {
    this.talleresService.getTalleres().subscribe({
      next: (data) => (this.talleres = data),
      error: (err) => (this.mensajeError = 'Error al cargar los talleres: ' + err.message)
    });
  }

  cargarParticipantes(): void {
    this.participantesService.getParticipantes().subscribe({
      next: (data) => (this.participantes = data),
      error: (err) => (this.mensajeError = 'Error al cargar los participantes: ' + err.message)
    });
  }

  inscribir(): void {
    this.mensajeError = '';
    this.mensajeExito = '';

    if (this.nuevaInscripcion.tallerId === 0 || this.nuevaInscripcion.participanteId === 0) {
      this.mensajeError = 'Debes seleccionar un taller y un participante';
      return;
    }

    this.inscripcionesService.crearInscripcion(this.nuevaInscripcion).subscribe({
      next: () => {
        this.mensajeExito = 'Inscripción registrada correctamente';
        this.cargarInscripciones();
        this.nuevaInscripcion = { tallerId: 0, participanteId: 0 };
      },
      error: (err) => (this.mensajeError = err.error?.mensaje || 'Error al inscribir: ' + err.message)
    });
  }

  cambiarEstado(inscripcion: Inscripcion): void {
    const nuevoEstado = inscripcion.estado === 'confirmada' ? 'cancelada' : 'confirmada';

    const payload: ActualizarInscripcion = {
      inscripcionId: inscripcion.inscripcionId,
      tallerId: inscripcion.tallerId,
      participanteId: inscripcion.participanteId,
      fechaInscripcion: inscripcion.fechaInscripcion,
      estado: nuevoEstado
    };

    this.inscripcionesService.actualizarInscripcion(inscripcion.inscripcionId, payload).subscribe({
      next: () => this.cargarInscripciones(),
      error: (err) => (this.mensajeError = 'Error al cambiar estado: ' + (err.error?.mensaje || err.message))
    });
  }

  eliminar(id: number): void {
    if (!confirm('¿Seguro que deseas cancelar/eliminar esta inscripción?')) {
      return;
    }

    this.inscripcionesService.eliminarInscripcion(id).subscribe({
      next: () => this.cargarInscripciones(),
      error: (err) => (this.mensajeError = 'Error al eliminar: ' + err.message)
    });
  }
}
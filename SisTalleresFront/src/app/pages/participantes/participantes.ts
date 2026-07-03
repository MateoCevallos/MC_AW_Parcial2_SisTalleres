import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Participantes as ParticipantesService } from '../../services/participantes';
import { Participante } from '../../models/participante.model';

@Component({
  selector: 'app-participantes',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './participantes.html',
  styleUrl: './participantes.css'
})
export class Participantes implements OnInit {
  participantes: Participante[] = [];
  participanteActual: Participante = this.participanteVacio();
  editando = false;
  mensajeError = '';

  constructor(private participantesService: ParticipantesService) {}

  ngOnInit(): void {
    this.cargarParticipantes();
  }

  cargarParticipantes(): void {
    this.participantesService.getParticipantes().subscribe({
      next: (data) => (this.participantes = data),
      error: (err) => (this.mensajeError = 'Error al cargar los participantes: ' + err.message)
    });
  }

  guardar(): void {
    this.mensajeError = '';

    if (this.editando) {
      this.participantesService
        .actualizarParticipante(this.participanteActual.participanteId, this.participanteActual)
        .subscribe({
          next: () => {
            this.cargarParticipantes();
            this.cancelar();
          },
          error: (err) => (this.mensajeError = 'Error al actualizar: ' + this.extraerError(err))
        });
    } else {
      this.participantesService.crearParticipante(this.participanteActual).subscribe({
        next: () => {
          this.cargarParticipantes();
          this.cancelar();
        },
        error: (err) => (this.mensajeError = 'Error al crear: ' + this.extraerError(err))
      });
    }
  }

  editar(participante: Participante): void {
    this.participanteActual = { ...participante };
    this.editando = true;
  }

  eliminar(id: number): void {
    if (!confirm('¿Seguro que deseas eliminar este participante?')) {
      return;
    }

    this.participantesService.eliminarParticipante(id).subscribe({
      next: () => this.cargarParticipantes(),
      error: (err) => (this.mensajeError = 'Error al eliminar: ' + this.extraerError(err))
    });
  }

  cancelar(): void {
    this.participanteActual = this.participanteVacio();
    this.editando = false;
  }

  private extraerError(err: any): string {
    // Caso 1: nuestros mensajes personalizados, ej. { mensaje: "..." }
    if (err.error?.mensaje) {
      return err.error.mensaje;
    }

    // Caso 2: errores de validación automáticos de .NET, ej. { errors: { Email: [...] } }
    if (err.error?.errors) {
      const primerCampo = Object.keys(err.error.errors)[0];
      const primerMensaje = err.error.errors[primerCampo][0];
      return primerMensaje;
    }

    // Caso 3: mensaje genérico de HttpClient como respaldo
    return err.message;
}

  private participanteVacio(): Participante {
    return {
      participanteId: 0,
      nombre: '',
      apellido: '',
      email: '',
      telefono: ''
    };
  }
}
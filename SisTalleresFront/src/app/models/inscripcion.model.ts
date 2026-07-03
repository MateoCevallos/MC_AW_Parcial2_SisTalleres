export interface Inscripcion {
  inscripcionId: number;
  fechaInscripcion: string;
  estado: string;
  tallerId: number;
  tallerNombre: string;
  tallerFecha: string;
  tallerUbicacion: string;
  participanteId: number;
  participanteNombre: string;
  participanteApellido: string;
  participanteEmail: string;
}

// Interfaz para crear una nueva inscripción
export interface NuevaInscripcion {
  tallerId: number;
  participanteId: number;
}
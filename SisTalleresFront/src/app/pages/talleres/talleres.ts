import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Talleres as TalleresService } from '../../services/talleres';
import { Taller } from '../../models/taller.model';

@Component({
  selector: 'app-talleres',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './talleres.html',
  styleUrl: './talleres.css'
})
export class Talleres implements OnInit {
  talleres: Taller[] = [];
  tallerActual: Taller = this.tallerVacio();
  editando = false;
  mensajeError = '';

  constructor(private talleresService: TalleresService) {}

  ngOnInit(): void {
    this.cargarTalleres();
  }

  cargarTalleres(): void {
    this.talleresService.getTalleres().subscribe({
      next: (data) => (this.talleres = data),
      error: (err) => (this.mensajeError = 'Error al cargar los talleres: ' + err.message)
    });
  }

  guardar(): void {
    this.mensajeError = '';

    if (this.editando) {
      this.talleresService.actualizarTaller(this.tallerActual.tallerId, this.tallerActual).subscribe({
        next: () => {
          this.cargarTalleres();
          this.cancelar();
        },
        error: (err) => (this.mensajeError = 'Error al actualizar: ' + err.message)
      });
    } else {
      this.talleresService.crearTaller(this.tallerActual).subscribe({
        next: () => {
          this.cargarTalleres();
          this.cancelar();
        },
        error: (err) => (this.mensajeError = 'Error al crear: ' + err.message)
      });
    }
  }

  editar(taller: Taller): void {
    this.tallerActual = { ...taller };
    this.editando = true;
  }

  eliminar(id: number): void {
    if (!confirm('¿Seguro que deseas eliminar este taller?')) {
      return;
    }

    this.talleresService.eliminarTaller(id).subscribe({
      next: () => this.cargarTalleres(),
      error: (err) => (this.mensajeError = 'Error al eliminar: ' + err.message)
    });
  }

  cancelar(): void {
    this.tallerActual = this.tallerVacio();
    this.editando = false;
  }

  private tallerVacio(): Taller {
    return {
      tallerId: 0,
      nombre: '',
      descripcion: '',
      fecha: '',
      ubicacion: ''
    };
  }
}
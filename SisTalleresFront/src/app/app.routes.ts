import { Routes } from '@angular/router';
import { Talleres } from './pages/talleres/talleres';
import { Participantes } from './pages/participantes/participantes';
import { Inscripciones } from './pages/inscripciones/inscripciones';

export const routes: Routes = [
  { path: '', redirectTo: 'talleres', pathMatch: 'full' },
  { path: 'talleres', component: Talleres },
  { path: 'participantes', component: Participantes },
  { path: 'inscripciones', component: Inscripciones }
];
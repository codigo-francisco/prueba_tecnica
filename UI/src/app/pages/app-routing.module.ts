import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListarUsuariosComponent } from './usuarios/listar-usuarios/listar-usuarios.component';

/**
 * Rutas raices de la aplicación
 */
const routes: Routes = [
  {
    component: ListarUsuariosComponent,
    path: 'listar'
  },
  {
    redirectTo: 'listar',
    path: '**'
  }
];

/**
 * Enrutador principal, carga las rutaz raices
 */
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

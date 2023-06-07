import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListarUsuariosComponent } from './usuarios/listar-usuarios/listar-usuarios.component';
import { authGuard } from '../shared/guards/auth.guard';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';

/**
 * Rutas raices de la aplicación
 */
const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'register',
    component: RegisterComponent
  },
  {
    component: ListarUsuariosComponent,
    canActivate: [authGuard],
    path: 'listar'
  },
  {
    redirectTo: 'login',
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

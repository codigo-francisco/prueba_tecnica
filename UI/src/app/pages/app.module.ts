import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from'@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ListarUsuariosComponent } from './usuarios/listar-usuarios/listar-usuarios.component';
import { MaterialModule } from '../shared/material/material.module';
import { AgregarModificarUsuariosComponent } from './usuarios/agregar-modificar-usuarios/agregar-modificar-usuarios.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { PhonePipe } from '../shared/pipes/phone.pipe';
import { CargaCircularComponent } from './cargas/carga-circular/carga-circular.component';

/**
 * Modulo raiz de la Aplicación
 */
@NgModule({
  declarations: [
    AppComponent,
    ListarUsuariosComponent,
    AgregarModificarUsuariosComponent,
    PhonePipe,
    CargaCircularComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MaterialModule,
    SweetAlert2Module.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { GeneralResponse } from '../models/general-response';
import { Usuario } from '../models/usuarios';

@Injectable({
  providedIn: 'root'
})
export class UsuariosService {

  usuariosEndPoint = "usuarios/";

  constructor(private httpCliente: HttpClient) { }

  private construirURL(nameEndPoint: string) {
    return `${environment.apiUrl}${this.usuariosEndPoint}${nameEndPoint}`;
  }

  consultarUsuarios(): Observable<GeneralResponse<Usuario[]>> {
    let nameEndPoint = "listar";
    let urlConsulta = this.construirURL(nameEndPoint);
    return this.httpCliente.get<GeneralResponse<Usuario[]>>(urlConsulta);
  }

  guardar(usuario: Usuario): Observable<GeneralResponse<boolean>> {
    let nameEndPoint = "agregar";
    let urlAgregar = this.construirURL(nameEndPoint);
    return this.httpCliente.post<GeneralResponse<boolean>>(urlAgregar, usuario);
  }

  modificar(usuario: Usuario): Observable<GeneralResponse<boolean>> {
    let nameEndPoint = "actualizar";
    let urlActualizar = this.construirURL(nameEndPoint);
    return this.httpCliente.put<GeneralResponse<boolean>>(urlActualizar, usuario);
  }

  borrar(usuarioId: number): Observable<GeneralResponse<boolean>> {
    let nameEndPoint = `borrar/${usuarioId}`;
    let urlEliminar = this.construirURL(nameEndPoint);
    return this.httpCliente.delete<GeneralResponse<boolean>>(urlEliminar);
  }
}

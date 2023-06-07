import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { GeneralResponse } from '../models/general-response';
import { Usuario } from '../models/usuarios';

/**
 * Servicio para consultar la API de Usuarios
 */
@Injectable({
  providedIn: 'root'
})
export class UsuariosService {

  private usuariosEndPoint = "usuarios/";

  constructor(private httpCliente: HttpClient) { }

  /**
   * Método para construir una URL para las operaciones
   * @param nameEndPoint Nombre del endpoint que se va a utilizar
   * @returns La URL construida con la base y el end point de usuarios
   */
  private construirURL(nameEndPoint: string) {
    return `${environment.apiUrl}${this.usuariosEndPoint}${nameEndPoint}`;
  }

  /**
   * Método para listar todos los usuarios a través del EndPoint de Usuarios
   * @returns Un Observable de la consulta a la API Web
   */
  listarUsuarios(): Observable<GeneralResponse<Usuario[]>> {
    let nameEndPoint = "listar";
    let urlConsulta = this.construirURL(nameEndPoint);
    return this.httpCliente.get<GeneralResponse<Usuario[]>>(urlConsulta);
  }

  /**
   * Método para agregar un usuario a través del EndPoint de Usuarios
   * @param usuario Usuario que se va agregar a base de datos
   * @returns Un Observable con el resultado de la operación
   */
  agregar(usuario: Usuario): Observable<GeneralResponse<boolean>> {
    let nameEndPoint = "agregar";
    let urlAgregar = this.construirURL(nameEndPoint);
    return this.httpCliente.post<GeneralResponse<boolean>>(urlAgregar, usuario);
  }

  /**
   * Método para modificar un usuario a través del EndPoint de Usuarios
   * @param usuario Usuario a modificar en la base de datos
   * @returns Un Observable con el resultado de la operación
   */
  modificar(usuario: Usuario): Observable<GeneralResponse<boolean>> {
    let nameEndPoint = "actualizar";
    let urlActualizar = this.construirURL(nameEndPoint);
    return this.httpCliente.put<GeneralResponse<boolean>>(urlActualizar, usuario);
  }

  /**
   * Método para borrar un usuario a través del EndPoint de Usuarios
   * @param usuarioId Id del Usuario a borrar en base de datos
   * @returns Un Observable con el resultado de la operación
   */
  borrar(usuarioId: number): Observable<GeneralResponse<boolean>> {
    let nameEndPoint = `borrar/${usuarioId}`;
    let urlEliminar = this.construirURL(nameEndPoint);
    return this.httpCliente.delete<GeneralResponse<boolean>>(urlEliminar);
  }
}

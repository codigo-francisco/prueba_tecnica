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

  consultarUsuarios(): Observable<GeneralResponse<Usuario[]>> {
    let urlConsulta = "listar"
    return this.httpCliente.get<GeneralResponse<Usuario[]>>(`${environment.apiUrl}${this.usuariosEndPoint}${urlConsulta}`);
  }
}

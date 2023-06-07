import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { GeneralResponse } from '../models/general-response';
import { LoginUser } from '../models/login-user';
import { RegisterUser } from '../models/register-user';

/**
 * Clave(key) del token almacenado
 */
const TOKEN_NAME = "access_token";

/**
 * Servicios para el manejo de autenticaciones en la aplicación cliente
 */
@Injectable({
  providedIn: 'root'
})
export class AuthService {

  /**
   * Endpoint de Autenticación en la API
   */
  authEndPoint = "auth";

  constructor(private httpClient: HttpClient) { }

  /**
   * Método para generar una URL endpoint que incluya la parte base, la parte de la autenticación y la parte final del endpoint
   * @param endPoint Parte final a trabajar (login, register)
   * @returns Una url completa para hacer una solicitud a la API
   */
  private construirEndPoint(endPoint: string) {
    return `${environment.apiUrl}${this.authEndPoint}/${endPoint}`;
  }

  /**
   * Método para realizar un login al servidor
   * @param loginUser Usuario que va a realizar el login
   * @returns Un objeto observable que va a recibir la respuesta de al solicitud con token
   */
  login(loginUser: LoginUser) {
    let loginEndPoint = "login";
    let urlEndPoint = this.construirEndPoint(loginEndPoint);

    return this.httpClient.post<GeneralResponse<any>>(urlEndPoint, loginUser);
  }

  /**
   * Método para realizar un registro de usuario al servidor
   * @param registerUser Usuario a registrar en la aplicación
   * @returns Un objeto observable que va a recibir la respuesta de la solicitud
   */
  registrar(registerUser: RegisterUser) {
    let registerEndPoint = "register";
    let urlEndPoint = this.construirEndPoint(registerEndPoint);

    return this.httpClient.post<GeneralResponse<boolean>>(urlEndPoint, registerUser);
  }

  /**
   * Método para colocar un nuevo token en almacenamiento local
   * @param token El token a almacenar
   */
  setToken(token: string) {
    localStorage.setItem(TOKEN_NAME, token);
  }

  /**
   * Método para obtener el token que se encuentra almacenado
   * @returns El token almacenado
   */
  getToken(): string {
    return `Bearer ${localStorage.getItem(TOKEN_NAME)}`;
  }

  /**
   * Método para eliminar el token almacenado
   */
  logout() {
    localStorage.removeItem(TOKEN_NAME);
  }

  /**
   * Método que valida que se encuentre un token almacenado
   * @returns Si el token está almacenado o no
   */
  public isLogged() {
    return of(localStorage.getItem(TOKEN_NAME) != null);
  }
}

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { GeneralResponse } from '../models/general-response';
import { LoginUser } from '../models/login-user';
import { RegisterUser } from '../models/register-user';

const TOKEN_NAME = "access_token";

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  authEndPoint = "auth";

  constructor(private httpClient: HttpClient) { }

  private construirEndPoint(endPoint: string) {
    return `${environment.apiUrl}${this.authEndPoint}/${endPoint}`;
  }

  login(loginUser: LoginUser) {
    let loginEndPoint = "login";
    let urlEndPoint = this.construirEndPoint(loginEndPoint);

    return this.httpClient.post<GeneralResponse<any>>(urlEndPoint, loginUser);
  }

  registrar(registerUser: RegisterUser) {
    let registerEndPoint = "register";
    let urlEndPoint = this.construirEndPoint(registerEndPoint);

    return this.httpClient.post<GeneralResponse<boolean>>(urlEndPoint, registerUser);
  }

  setToken(token: string) {
    localStorage.setItem(TOKEN_NAME, token);
  }

  getToken(): string {
    return `Bearer ${localStorage.getItem(TOKEN_NAME)}`;
  }

  logout() {
    localStorage.removeItem(TOKEN_NAME);
  }

  public isLogged() {
    return of(localStorage.getItem(TOKEN_NAME) != null);
  }
}

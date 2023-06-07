import { Component, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginUser } from 'src/app/shared/models/login-user';
import { AuthService } from 'src/app/shared/services/auth.service';
import Swal from 'sweetalert2';
import { CargaCircularComponent } from '../../cargas/carga-circular/carga-circular.component';
import { take, tap, throwError } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  isLogging = false;
  form = new FormGroup({
    "username": new FormControl("", [Validators.required]),
    "password": new FormControl("", [Validators.required])
  });

  @ViewChild(CargaCircularComponent) cargaComponent?: CargaCircularComponent

  constructor(private authService: AuthService, private router: Router) { }

  getMessageError(control: FormControl, nombre: string): string {
    let message = "";
    if (control.errors) {
      if (control.errors['required']) {
        message = `El campo ${nombre} es requerido`;
      } else if (control.errors['maxLength']) {
        message = `El campo ${nombre} tiene un maximo de ${control.errors['maxlength'].requiredLength}`;
      } else if (control.errors['minLength']) {
        message = `El campo ${nombre} tiene un minimo de ${control.errors['minlength'].requiredLength}`;
      } else if (control.errors['pattern']) {
        message = `El campo ${nombre} no tiene el formato correcto`;
      }
    }
    return message;
  }

  login() {
    if (this.form.valid) {
      let loginUser: LoginUser = {
        userName: this.form.controls.username.value!,
        password: this.form.controls.password.value!
      }

      this.cargaComponent?.show("Autenticando","Se está realizando el proceso de autenticación, por favor espere");

      this.authService.login(loginUser).subscribe(response => {
        this.cargaComponent?.hide();

        if (!response.hasError) {
          this.authService.setToken(response.data?.token);
          this.router.navigateByUrl('listar');
        } else {
          console.error(`Error al tratar de hacer login, message: ${response.messageException}`);
          Swal.fire({
            title: response.messageError,
            icon: 'error'
          });
        }
      });
    }
  }
}

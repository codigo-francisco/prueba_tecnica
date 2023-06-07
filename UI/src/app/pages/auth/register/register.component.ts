import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { RegisterUser } from 'src/app/shared/models/register-user';
import { AuthService } from 'src/app/shared/services/auth.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  form = new FormGroup({
    "username": new FormControl("", [Validators.required]),
    "password": new FormControl("", [Validators.required]),
    "email": new FormControl("",[Validators.required, Validators.email]),
  });

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
      } else if (control.errors['pattern'] || control.errors['email']) {
        message = `El campo ${nombre} no tiene el formato correcto`;
      }
    }
    return message;
  }

  registrar() {
    if (this.form.valid) {
      let registerUser: RegisterUser = {
        email: this.form.controls.email.value!,
        password: this.form.controls.password.value!,
        userName: this.form.controls.username.value!
      }

      this.authService.registrar(registerUser).subscribe(response => {
        if (!response.hasError) {
          Swal.fire({
            title: 'Usuario registrado exitosamente, ya puede hacer login'
          }).then(() => {
            this.router.navigateByUrl('login');
          });
        } else {
          Swal.fire({
            title: response.messageError
          });
        }
      });
    }
  }
}

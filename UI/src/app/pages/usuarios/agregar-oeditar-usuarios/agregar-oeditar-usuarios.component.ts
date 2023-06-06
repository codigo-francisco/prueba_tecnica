import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { AgregarOEditarData } from 'src/app/shared/models/agregar-oeditar-data';
import { Usuario } from 'src/app/shared/models/usuarios';
import { UsuariosService } from 'src/app/shared/services/usuarios.service';
import Swal from 'sweetalert2';

const regexCurp = /^[A-Z]{4}\d{6}[HM][A-Z]{5}\d{2}$/;
const regexPhone = /^\d{10}$/;

@Component({
  selector: 'app-agregar-oeditar-usuarios',
  templateUrl: './agregar-oeditar-usuarios.component.html',
  styleUrls: ['./agregar-oeditar-usuarios.component.scss']
})
export class AgregarOEditarUsuariosComponent implements OnInit {
  idUsuario = 0;
  accion = "Agregar"
  form = new FormGroup({
    "nombre": new FormControl("", [Validators.required, Validators.maxLength(100)]),
    "apellidoPaterno": new FormControl("", [Validators.required, Validators.maxLength(100)]),
    "apellidoMaterno": new FormControl("", [Validators.required, Validators.maxLength(100)]),
    "salario": new FormControl("", [Validators.required]),
    "curp": new FormControl("", [Validators.required, Validators.pattern(regexCurp)]),
    "telefono": new FormControl("", [Validators.required, Validators.pattern(regexPhone)])
  });

  constructor(
    private usuariosService: UsuariosService,
    private dialogRef: MatDialogRef<AgregarOEditarUsuariosComponent>,
    @Inject(MAT_DIALOG_DATA) private data: AgregarOEditarData
  ) { }

  ngOnInit(): void {
    if (this.data) {
      let data = this.data;
      let usuario = data.usuario;
      this.accion = data.accion;
      this.idUsuario = usuario?.id!;
      
      let controls = this.form.controls;
      controls.nombre.setValue(usuario?.nombre!);
      controls.apellidoPaterno.setValue(usuario?.apellidoPaterno!);
      controls.apellidoMaterno.setValue(usuario?.apellidoMaterno!);
      controls.salario.setValue(usuario?.salario?.toString()!);
      controls.curp.setValue(usuario?.curp!);
      controls.telefono.setValue(usuario?.telefono!);
    }
  }

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

  guardarOEditar() {
    if (this.form.valid) {
      if (this.accion == "Agregar") {
        this.guardar();
      } else if (this.accion == "Editar") {
        this.editar();
      }
    }
  }

  generarUsuario(): Usuario {
    const usuario: Usuario = {
      nombre: this.form.controls.nombre.value!,
      apellidoPaterno: this.form.controls.apellidoPaterno.value!,
      apellidoMaterno: this.form.controls.apellidoMaterno.value!,
      salario: +this.form.controls.salario.value!,
      curp: this.form.controls.curp.value!.toUpperCase(),
      telefono: this.form.controls.telefono.value!
    };

    if (this.idUsuario != 0) {
      usuario.id = this.idUsuario;
    }

    return usuario;
  }

  guardar() {
    const usuario = this.generarUsuario();

    this.usuariosService.guardar(usuario).subscribe(response => {
      if (!response.hasError) {
        Swal.fire({
          title: "Usuario agregado",
          icon: 'success',          
          showConfirmButton: true,
          confirmButtonText: 'Ok'
        }).then(() => {
          this.dialogRef.close({result: 'ok'});
        });
      } else {
        console.error(`error al tratar de guardar un usuario mensaje: ${response.messageException}, code: ${response.httpCode}`);
        Swal.fire(response.messageError, undefined, 'error');
      }
    });
  }

  editar() {
    const usuario = this.generarUsuario();

    this.usuariosService.modificar(usuario).subscribe(response => {
      if (!response.hasError) {
        Swal.fire({
          title: "Usuario modificado",
          icon: 'success',          
          showConfirmButton: true,
          confirmButtonText: 'Ok'
        }).then(() => {
          this.dialogRef.close({result: 'ok'});
        });
      } else {
        console.error(`error al tratar de guardar un usuario mensaje: ${response.messageException}, code: ${response.httpCode}`);
        Swal.fire(response.messageError, undefined, 'error');
      }
    });
  }
}

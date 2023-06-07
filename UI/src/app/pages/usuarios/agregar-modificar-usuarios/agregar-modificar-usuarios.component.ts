import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { AgregarEditarData } from 'src/app/shared/models/agregar-editar-data';
import { Usuario } from 'src/app/shared/models/usuarios';
import { UsuariosService } from 'src/app/shared/services/usuarios.service';
import Swal from 'sweetalert2';
import { CargaCircularComponent } from '../../cargas/carga-circular/carga-circular.component';

/**
 * Expresión regular para una CURP en mayusculas
 */
const regexCurp = /^[A-Z]{4}\d{6}[HM][A-Z]{5}\d{2}$/;
/**
 * Expresión regular para un telefono de 10 digitos
 */
const regexPhone = /^\d{10}$/;

/**
 * Componente para el agregado o modificación de un Usuario
 */
@Component({
  selector: 'app-agregar-modificar-usuarios',
  templateUrl: './agregar-modificar-usuarios.component.html',
  styleUrls: ['./agregar-modificar-usuarios.component.scss']
})
export class AgregarModificarUsuariosComponent implements OnInit {
  /**
   * Esta variable es para manejar los mensajes de carga a través de un componente
   */
  @ViewChild(CargaCircularComponent) cargaComponent?: CargaCircularComponent;
  
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
    private dialogRef: MatDialogRef<AgregarModificarUsuariosComponent>,
    @Inject(MAT_DIALOG_DATA) private data: AgregarEditarData
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

  /**
   * Método para obtener el texto del mensaje de error de acuerdo al tipo de error que contiene el control
   * @param control control que se analizara si tiene errores
   * @param nombre nombre del control
   * @returns mensaje de error en caso de haber alguno
   */
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

  /**
   * Método que activa el boton del formulario, agrega o edita acorde a la acción del componente
   */
  guardarOModificar() {
    if (this.form.valid) {
      if (this.accion == "Agregar") {
        this.agregar();
      } else if (this.accion == "Editar") {
        this.modificar();
      }
    }
  }

  /**
   * Metodó para generar un Usuario a partir de los datos en el formulario
   * @returns Usuario que se va a enviar a través de la API Web
   */
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

  /**
   * Método para pasar a mayusculas la CURP escrita por el usuario cuando abandone el foco del texto
   */
  curpBlur() {
    this.form.controls.curp.setValue(this.form.controls.curp.value?.toUpperCase() ?? '');
  }

  /**
   * Método para agregar un nuevo Usuario
   */
  agregar() {
    const usuario = this.generarUsuario();

    this.cargaComponent?.show("Agregando al usuario", "Se está realizando la operación, por favor espere");

    this.usuariosService.agregar(usuario).subscribe(response => {
      this.cargaComponent?.hide();
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

  /**
   * Método para editar modificar un usuario
   */
  modificar() {
    const usuario = this.generarUsuario();

    this.cargaComponent?.show("Modficando al usuario", "Se está realizando la operación, por favor espere");

    this.usuariosService.modificar(usuario).subscribe(response => {
      this.cargaComponent?.hide();
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

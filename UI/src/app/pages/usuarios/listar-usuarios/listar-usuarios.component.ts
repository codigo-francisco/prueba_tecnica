import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Usuario } from 'src/app/shared/models/usuarios';
import { UsuariosService } from 'src/app/shared/services/usuarios.service';
import { AgregarOEditarUsuariosComponent } from '../agregar-oeditar-usuarios/agregar-oeditar-usuarios.component';
import { AgregarOEditarData } from 'src/app/shared/models/agregar-oeditar-data';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-listar-usuarios',
  templateUrl: './listar-usuarios.component.html',
  styleUrls: ['./listar-usuarios.component.scss']
})
export class ListarUsuariosComponent implements OnInit {
  displayedColumns = ["nombre","apellidoPaterno","apellidoMaterno","salario","curp","telefono","editar","eliminar"];
  usuariosSource: Usuario[] = [];

  constructor(
    private usuariosService: UsuariosService, 
    private dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.cargarUsuarios();
  }

  cargarUsuarios() {
    this.usuariosService.consultarUsuarios().subscribe(response => {
      if (!response.hasError) {
        this.usuariosSource = [];
        this.usuariosSource.push(... response.data!);
      } else {
        console.error(`error en consulta de usuarios, mensaje: ${response.messageError} codigo: ${response.httpCode}`);
      }
    });
  }

  agregarUsuario() {
    let dialog = this.dialog.open<AgregarOEditarUsuariosComponent>(AgregarOEditarUsuariosComponent);
    
    dialog.afterClosed().subscribe(data => {
      if (data.result == "ok") {
        this.cargarUsuarios();
      }
    });
  }

  editarUsuario(usuario: Usuario) {
    let dialog = this.dialog.open<AgregarOEditarUsuariosComponent, AgregarOEditarData>(AgregarOEditarUsuariosComponent, {
      data: {
        usuario: usuario,
        accion: "Editar"
      }
    });

    dialog.afterClosed().subscribe(data => {
      if (data.result == "ok") {
        this.cargarUsuarios();
      }
    });
  }

  eliminarUsuario(usuario: Usuario) {
    Swal.fire({
      title: `¿Desea eliminar al usuario ${usuario.nombre} ${usuario.apellidoPaterno} ${usuario.apellidoMaterno}?`,
      html: 'Esta es una acción irreversible',
      showConfirmButton: true,
      confirmButtonText: 'SI',
      showDenyButton: true,
      denyButtonText: 'NO'
    }).then(result => {
      if (result.isConfirmed) {
        this.usuariosService.borrar(usuario.id!).subscribe(response => {
          if (!response.hasError) {
            Swal.fire({
              title: "Usuario eliminado",
              icon: 'success',          
              showConfirmButton: true,
              confirmButtonText: 'Ok'
            }).then(() => {
              this.cargarUsuarios();
            });
          } else {
            console.error(`error al tratar de guardar un usuario mensaje: ${response.messageError}, code: ${response.httpCode}`);
            Swal.fire("Ocurrió un error, intente más tarde", undefined, 'error');
          }
        });
      }
    });
  }
}

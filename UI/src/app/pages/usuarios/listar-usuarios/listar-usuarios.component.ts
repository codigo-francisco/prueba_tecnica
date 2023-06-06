import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Usuario } from 'src/app/shared/models/usuarios';
import { UsuariosService } from 'src/app/shared/services/usuarios.service';
import { AgregarOEditarUsuariosComponent } from '../agregar-oeditar-usuarios/agregar-oeditar-usuarios.component';
import { AgregarOEditarData } from 'src/app/shared/models/agregar-oeditar-data';
import Swal from 'sweetalert2';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { DialogResponse } from 'src/app/shared/models/dialog-response';

@Component({
  selector: 'app-listar-usuarios',
  templateUrl: './listar-usuarios.component.html',
  styleUrls: ['./listar-usuarios.component.scss']
})
export class ListarUsuariosComponent implements OnInit {
  displayedColumns = ["nombre","apellidoPaterno","apellidoMaterno","salario","curp","telefono","editar","eliminar"];
  usuariosSource = new MatTableDataSource<Usuario>([]);
  @ViewChild(MatPaginator) paginator?: MatPaginator;
  @ViewChild(MatSort) sort?: MatSort;

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
        this.usuariosSource.data = [];
        this.usuariosSource.data.push(... response.data!);
        this.usuariosSource.paginator = this.paginator!;
        this.usuariosSource.sort = this.sort!;
      } else {
        console.error(`error en consulta de usuarios, mensaje: ${response.messageException} codigo: ${response.httpCode}`);
        Swal.fire({
          title: response.messageError,
          icon: 'error'
        });
      }
    });
  }

  agregarUsuario() {
    let dialog = this.dialog.open<AgregarOEditarUsuariosComponent, AgregarOEditarData, DialogResponse>(AgregarOEditarUsuariosComponent);
    
    dialog.afterClosed().subscribe(data => {
      if (data?.result == "ok") {
        this.cargarUsuarios();
      }
    });
  }

  editarUsuario(usuario: Usuario) {
    let dialog = this.dialog.open<AgregarOEditarUsuariosComponent, AgregarOEditarData, DialogResponse>(AgregarOEditarUsuariosComponent, {
      data: {
        usuario: usuario,
        accion: "Editar"
      }
    });

    dialog.afterClosed().subscribe(data => {
      if (data?.result == "ok") {
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
            console.error(`error al tratar de guardar un usuario mensaje: ${response.messageException}, code: ${response.httpCode}`);
            Swal.fire(response.messageError, undefined, 'error');
          }
        });
      }
    });
  }
}

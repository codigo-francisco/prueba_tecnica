import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Usuario } from 'src/app/shared/models/usuarios';
import { UsuariosService } from 'src/app/shared/services/usuarios.service';
import { AgregarModificarUsuariosComponent } from '../agregar-modificar-usuarios/agregar-modificar-usuarios.component';
import { AgregarEditarData } from 'src/app/shared/models/agregar-editar-data';
import Swal from 'sweetalert2';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { DialogResponse } from 'src/app/shared/models/dialog-response';
import { CargaCircularComponent } from '../../cargas/carga-circular/carga-circular.component';
import { AuthService } from 'src/app/shared/services/auth.service';
import { Router } from '@angular/router';

/**
 * Componente que lista a los usuarios y realiza acciones para la manipulación de los usuarios
 */
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
  @ViewChild(CargaCircularComponent) cargaComponent?: CargaCircularComponent;

  constructor(
    private usuariosService: UsuariosService, 
    private dialog: MatDialog,
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit(): void {
  }

  ngAfterViewInit() {
    setTimeout(()=> {
      this.cargarUsuarios();
    });
  }

  /**
   * Método para cargar todos los usuarios de base de datos
   */
  cargarUsuarios() {
    this.cargaComponent?.show("Cargando Usuarios","Se estan cargando los usuarios, por favor espere");
    
    this.usuariosService.listarUsuarios().subscribe(response => {
      this.cargaComponent?.hide();
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

  /**
   * Método para agregar usuarios a través del dialogo de Agregado y Modificado de Usuarios
   */
  agregarUsuario() {
    let dialog = this.dialog.open<AgregarModificarUsuariosComponent, AgregarEditarData, DialogResponse>(AgregarModificarUsuariosComponent);
    
    dialog.afterClosed().subscribe(data => {
      if (data?.result == "ok") {
        this.cargarUsuarios();
      }
    });
  }

  /**
   * Método para modificar usuarios a través del dialogo de Agregado y Modificado de Usuarios
   * @param usuario Usuario que se va a editar
   */
  modificarUsuario(usuario: Usuario) {
    let dialog = this.dialog.open<AgregarModificarUsuariosComponent, AgregarEditarData, DialogResponse>(AgregarModificarUsuariosComponent, {
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

  /**
   * Método que invoca un cuadro de dialogo para confirmar la eliminación del Usuario seleccionado
   * @param usuario Usuario a eliminar
   */
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
        this.cargaComponent?.show("Eliminando Usuario","Se está realizando la operación, por favor espere.");
        this.usuariosService.borrar(usuario.id!).subscribe(response => {
          this.cargaComponent?.hide();
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

  /**
   * Evento para cerrar sesión de la aplicación
   */
  cerrarSesion() {
    this.authService.logout();
    this.router.navigateByUrl('login');
  }
}

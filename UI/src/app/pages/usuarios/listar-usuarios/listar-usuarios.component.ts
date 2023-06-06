import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Usuario } from 'src/app/shared/models/usuarios';
import { UsuariosService } from 'src/app/shared/services/usuarios.service';
import { AgregarOEditarUsuariosComponent } from '../agregar-oeditar-usuarios/agregar-oeditar-usuarios.component';

@Component({
  selector: 'app-listar-usuarios',
  templateUrl: './listar-usuarios.component.html',
  styleUrls: ['./listar-usuarios.component.scss']
})
export class ListarUsuariosComponent implements OnInit {
  displayedColumns = ["nombre","apellidoPaterno","apellidoMaterno","salario","curp","telefono"];
  usuariosSource: Usuario[] = [];

  constructor(
    private usuariosService: UsuariosService, 
    private dialog: MatDialog
  ) { }

  ngOnInit(): void {
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
    let dialog = this.dialog.open(AgregarOEditarUsuariosComponent);
    
    dialog.afterClosed().subscribe(result => {
      
    });
  }
}

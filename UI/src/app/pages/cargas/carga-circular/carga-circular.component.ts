import { Component, Inject } from '@angular/core';

/**
 * Componente para mostrar una pantalla de carga
 */
@Component({
  selector: 'app-carga-circular',
  templateUrl: './carga-circular.component.html',
  styleUrls: ['./carga-circular.component.scss']
})
export class CargaCircularComponent {
  hideRoot = true;
  titulo = "titulo";
  mensaje = "mensaje";

  constructor(
  ) { }

  public show(titulo: string, mensaje: string) {
    console.log(titulo, mensaje);
    this.titulo = titulo;
    this.mensaje = mensaje;
    this.toggle();
  }

  public hide() {
    this.titulo = "";
    this.mensaje = "";
    this.hideRoot = true;
  }

  /**
   * Método para ocultar/mostrar el componente de carga
   */
  public toggle() {
    this.hideRoot = !this.hide;
  }
}

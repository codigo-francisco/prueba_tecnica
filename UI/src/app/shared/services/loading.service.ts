import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

/**
 * Servicio para el manejo de dialogos de carga que se queden abiertas en errores o fallos
 */
@Injectable({
  providedIn: 'root'
})
export class LoadingService {

  /**
   * Esta variable almacena un behaviorsubject que mandara los streams para cerrar o no los dialoso
   */
  private bs = new BehaviorSubject<boolean>(false);

  /**
   * Propiedad para obtener el stream de cierre de dialogos
   */
  get SendState() {
    return this.bs;
  }

  constructor() { }

  /**
   * Mostrar dialogos (sin implementación)
   */
  show() {
    this.bs.next(false);
  }

  /**
   * Esconder todas los dialoso
   */
  hide() {
    this.bs.next(true);
  }
}

import { Pipe, PipeTransform } from '@angular/core';

/**
 * Pipe para transforma una cadena de telefono en el formato (lada) numero
 */
@Pipe({
  name: 'phone'
})
export class PhonePipe implements PipeTransform {
  /**
   * Método para transformar una cadena en telefono con formato: (lada) numero
   * @param value Telefono a transformar
   * @param args No se utiliza
   * @returns Telefono transformado en el formato indicado
   */
  transform(value: string, ...args: any[]): string {
    return `(${value.substring(0, 3)}) ${value.substring(3)}`;
  }

}

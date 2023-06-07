/**
 * Interfaz para representar el DTO del Usuario
 */
export interface Usuario {
    id?: number;
    nombre: string;
    apellidoPaterno: string;
    apellidoMaterno: string;
    salario: number;
    curp: string;
    telefono: string;
}

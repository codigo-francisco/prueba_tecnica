import { Usuario } from "./usuarios";

/**
 * Interfaz que representa los datos que se envian al dialogo de Agregar o Editar Usuarios
 */
export interface AgregarEditarData {
    /**
     * Usuario a editar (solo edición)
     */
    usuario?: Usuario;
    /**
     * Accion a realizar en el dialogo
     */
    accion: 'Agregar' | 'Editar';
}

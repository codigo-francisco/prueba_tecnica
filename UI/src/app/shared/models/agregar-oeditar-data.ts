import { Usuario } from "./usuarios";

export interface AgregarOEditarData {
    usuario?: Usuario;
    accion: string;
}

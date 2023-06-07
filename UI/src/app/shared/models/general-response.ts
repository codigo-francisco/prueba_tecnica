/**
 * Interfaz para representar la respuesta general que se recibe del servidor
 * 
 * @type T tipo de datos que contendra la respuesta
 */
export interface GeneralResponse<T> {
    /**
     * Indica si contiene un error o no
     */
    hasError: boolean;
    /**
     * Mensaje de error para el cliente
     */
    messageError?: string;
    /**
     * Mensaje de la excepción generada
     */
    messageException?: string;
    /**
     * Codigo HTTP de respuesta
     */
    httpCode: number;
    /**
     * Datos enviados en la respuesta
     */
    data?: T;
}

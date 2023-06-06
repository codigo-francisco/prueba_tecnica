export interface GeneralResponse<T> {
    hasError: boolean;
    messageError?: string;
    httpCode: number;
    data?: T;
}

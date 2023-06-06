export interface GeneralResponse<T> {
    hasError: boolean;
    messageError?: string;
    messageException?: string;
    httpCode: number;
    data?: T;
}

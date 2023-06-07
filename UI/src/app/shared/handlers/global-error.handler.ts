import { HttpErrorResponse } from "@angular/common/http";
import { ErrorHandler, Injectable, NgZone } from "@angular/core";
import Swal from "sweetalert2";
import { AuthService } from "../services/auth.service";
import { Router } from "@angular/router";
import { LoadingService } from "../services/loading.service";

@Injectable()
export class GlobalErrorHandler implements ErrorHandler {
  constructor(
    private zone: NgZone,
    private authService: AuthService,
    private router: Router,
    private loadingService: LoadingService
  ) {}

  handleError(error: Error | HttpErrorResponse | any) {
    this.loadingService.hide();
    
    let status = error?.status;
    
    this.zone.run(() => {
        let message: string;
        if (status == 401) {
            this.authService.logout();
            message = "Su sesión ha caducado, se rediccionara al login";
            return Swal.fire(message).then(() => this.router.navigateByUrl('login'));
        } else {
            message = "Ha ocurrido un error desconocido por favor intente más tarde";
            return Swal.fire(message);
        }
    });

    console.error('Error from global error handler', error);
  }
}
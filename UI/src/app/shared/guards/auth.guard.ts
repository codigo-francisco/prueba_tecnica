import { Injectable, inject } from '@angular/core';
import { ActivatedRouteSnapshot, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable, tap } from 'rxjs';
import { AuthService } from '../services/auth.service';

export const authGuard = () => {
  const router = inject(Router);
  const service = inject(AuthService);
  
  return service.isLogged()
    .pipe(
      tap(value => {
        return !value ? router.navigate(['/login']) : false;
      })
    );
}
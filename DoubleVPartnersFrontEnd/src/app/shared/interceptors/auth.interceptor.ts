import { HttpErrorResponse, type HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { StorageService } from '../services/storage.service';
import { tap } from 'rxjs';
import { MessageService } from 'primeng/api';
import { AuthStorage } from '../constans/storage.constant';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router)
  const storage = inject(StorageService)
  const message = inject(MessageService)

  if (req.url.includes('/Auth')) {
    return next(req);
  }
  
  const newReq = req.clone({
    headers: req.headers
      .append('Authorization', `Bearer ${storage.get(AuthStorage.TOKEN)}`)
  })

  return next(newReq)
    .pipe(tap({
      next: (resp) => {}, 
      error: (err) => {
        let errorMessage = { severity: 'error', summary: 'Atención', detail: 'Un error desconocido ha ocurrido' };

        if (err instanceof HttpErrorResponse) {
          errorMessage = {
            ...errorMessage,
            detail: err.error.title
          }

          if (err.status === 401) {
            // TODO: Refresh token get
            errorMessage = {
              ...errorMessage,
              detail: 'Refrescando token'
            }
            storage.clear();
            router.navigate(['login']);
          }            
        }

        message.add(errorMessage);
      },
      complete: () => {},
      finalize: () => { },
    }));
};

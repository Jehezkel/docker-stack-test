import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { mergeMap } from 'rxjs';

export const AuthInterceptor: HttpInterceptorFn = (req, next) => {
  var authService = inject(AuthService);

  return authService.getAccessTokenSilently().pipe(
    mergeMap(token => {
      const authorizedRequest = req.clone({
        headers: req.headers.set('Authorization', `Bearer ${token}`)
      });
      return next(authorizedRequest);
    }));
}

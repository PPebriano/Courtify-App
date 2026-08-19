import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const token = authService.getToken();

  if (req.url.includes('/api/login')) {
    return next(req);
  }

  if (token) {
    req = req.clone({
      setHeaders: {
        Authrization: `Bearer ${token}`,
      },
    });
  }

  return next(req);
};

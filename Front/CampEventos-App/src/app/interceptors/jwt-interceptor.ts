import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { switchMap, take } from 'rxjs';
import { AccountService } from '../services/account.service';

export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
  const accountService = inject(AccountService);

  return accountService.currentUser$.pipe(
    take(1),
    switchMap(user => {
      if (!user?.token) {
        return next(req);
      }

      const authReq  = req.clone({
        setHeaders: {
          Authorization: `Bearer ${user.token}`
        }
      });

      return next(authReq );
    })
  );
};
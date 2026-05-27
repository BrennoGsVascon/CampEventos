import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);

  const userJson = localStorage.getItem('user');

  if (userJson) {
    return true;
  }

  router.navigateByUrl('/user/login');
  return false;
};

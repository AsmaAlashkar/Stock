import { CanActivateFn } from '@angular/router';
import { inject } from '@angular/core';
import { AccountService } from 'src/app/account/account.service';
import { Router } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService) as AccountService;  // Cast to AccountService
  const router = inject(Router);  // Inject Router

  if (accountService.isLoggedIn()) {  // Check if the user is logged in
    return true;
  } else {
    router.navigate(['/login']);  // Redirect to login if not authenticated
    return false;
  }
};

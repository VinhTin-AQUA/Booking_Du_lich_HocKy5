import { CanActivateFn, Router } from '@angular/router';
import jwt_decode from 'jwt-decode';
import { inject } from '@angular/core';
import { AccountService } from 'src/app/account/account.service';
import { Roles } from './roles';

export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const accountService = inject(AccountService);
  const roleSys = route.data['role'];
  let jwt = accountService.getJwtUser();

  if (jwt === null) {
    jwt = '';

    if (roleSys !== undefined) {
      return router.navigateByUrl('/access-denied');
    }
    return true;
  }
  const decoded: any = jwt_decode(jwt);
  const roleAccount = decoded.role;

  if (roleSys === undefined && roleAccount !== Roles.USER) {
    if (roleAccount === Roles.ADMIN) {
      return router.navigateByUrl('/admin');
    }

    if (roleAccount === Roles.EMPLOYEE) {
      return router.navigateByUrl('/employee');
    }

    if (roleAccount === Roles.AGENTHOTEL) {
      return router.navigateByUrl('/agent/post');
    }

    if (roleAccount === Roles.AGENTTOUR) {
      return router.navigateByUrl('/agent-tour/manage-tour');
    }
  }

  if (roleAccount !== roleSys && roleAccount !== Roles.USER) {
    return router.navigateByUrl('/access-denied');
  }
  
  return true;
};

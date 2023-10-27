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

  if (roleSys === undefined) {
    if (roleAccount === Roles.ADMIN) {
      return router.navigateByUrl('/admin');
    }

    if (roleAccount === Roles.EMPLOYEE) {
      return router.navigateByUrl('/employee');
    }

<<<<<<< HEAD
    if (roleAccount === Roles.AGENTHOTEL) {
      return router.navigateByUrl('/agent');
    }

    if (roleAccount === Roles.AGENTTOUR) {
      return router.navigateByUrl('/agent-tour');
    }
=======
    if (roleAccount === Roles.AGENT) {
      return router.navigateByUrl('/agent');
    }
>>>>>>> fbf7a2fbe52bc67243c690eef5a99389b5d4f982
  }

  if (roleAccount !== roleSys) {
    return router.navigateByUrl('/access-denied');
  }
  return true;
};

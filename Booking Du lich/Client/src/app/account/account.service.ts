import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { ReplaySubject, map, of } from 'rxjs';

import { environment } from 'src/environments/environment.development';
import { RegisterUser } from '../shared/models/account/reigisterUser';
import { LoginUser } from '../shared/models/account/loginUser';
import { ConfirmEmail } from '../shared/models/account/confirmEmail';
import { User } from '../shared/models/account/user';
import { ResendEmail } from '../shared/models/account/resendEmail';
import { ResetPassword } from '../shared/models/account/resetPassword';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  private userSource: ReplaySubject<User | null> = new ReplaySubject(1);
  user$ = this.userSource.asObservable();

  constructor(private http: HttpClient, private router: Router) {}

  signUp(model: RegisterUser) {
    return this.http.post<User>(
      `${environment.appUrl}/authentication/sign-up`,
      model
    );
  }

  confirmEmail(model: ConfirmEmail) {
    return this.http.put(
      `${environment.appUrl}/authentication/confirm-email`,
      model
    );
  }

  login(model: LoginUser, remmeberMe: boolean) {
    return this.http
      .post<User>(`${environment.appUrl}/authentication/login`, model)
      .pipe(
        map((user: User) => {
          //if (user && remmeberMe === true) {
          this.saveJWTUser(user);
          //}
        })
      );
  }

  notLogin() {
    this.userSource.next(null);
    this.router.navigateByUrl('/');
  }

  logout() {
    localStorage.removeItem(environment.userKey);
    this.userSource.next(null);
    this.router.navigateByUrl('/');
  }

  private saveJWTUser(user: User) {
    localStorage.setItem(environment.userKey, JSON.stringify(user));
    this.userSource.next(user);
  }

  refreshUser(jwt: string | null) {
    if (jwt === null) {
      this.userSource.next(null);
      return of(undefined);
    }

    let headers = new HttpHeaders();
    headers = headers.set('Authorization', 'Bearer ' + jwt);

    return this.http
      .get<User>(`${environment.appUrl}/authentication/refresh-user-token`, {
        headers: headers,
      })
      .pipe(
        map((user: User) => {
          if (user) {
            this.saveJWTUser(user);
          }
        })
      );
  }

  getJwtUser() {
    const key = localStorage.getItem(environment.userKey);
    if (key) {
      const user: User = JSON.parse(key);
      return user.jwt;
    }
    return null;
  }

  resendEmailConfirm(model: ResendEmail) {
    return this.http.post(
      `${environment.appUrl}/authentication/resent-email`,
      model
    );
  }

  forgotPassword(email: string) {
    return this.http.post(
      `${environment.appUrl}/authentication/forgot-password/${email}`,
      null
    );
  }

  resetPassword(model: ResetPassword) {
    return this.http.put(
      `${environment.appUrl}/authentication/reset-password`,
      model
    );
  }
}

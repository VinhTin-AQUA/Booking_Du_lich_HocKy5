import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { environment } from 'src/environments/environment.development';
import { RegisterUser } from '../shared/models/account/reigisterUser';
import { LoginUser } from '../shared/models/account/loginUser';
import { ConfirmEmail } from '../shared/models/account/confirmEmail';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  constructor(private http: HttpClient) {}

  signIn(model: RegisterUser) {
    return this.http.post(
      `${environment.appUrl}/authentication/sign-up`,
      model
    );
  }

  confirmEmail(model: ConfirmEmail) {
    return this.http.put(
      `${environment.appUrl}/authentication/confirm-email`, model
    );
  }

  login(model: LoginUser) {
    return this.http.post(`${environment.appUrl}/authentication/login`, model);
  }
}

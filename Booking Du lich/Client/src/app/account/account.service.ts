import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { environment } from 'src/environments/environment.development';
import { RegisterUser } from '../shared/models/account/reigisterUser';

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
}

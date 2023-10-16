import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { AccountService } from '../account.service';
import { LoginUser } from 'src/app/shared/models/account/loginUser';
import { SharedService } from 'src/app/shared/shared.service';
import { getLocaleMonthNames } from '@angular/common';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  loginForm: FormGroup = new FormGroup([]);
  rememberMe: boolean = true;
  submitted: boolean = false;
  errorMessage: string = '';

  constructor(
    private formBuilder: FormBuilder,
    private accountService: AccountService,
    private sharedService: SharedService,
    private router: Router
  ) {
    accountService.user$.subscribe({
      next: (res) => {
        if (res !== null) {
          this.router.navigateByUrl('');
        }
      },
    });
    this.loginForm = formBuilder.group({
      email: ['', [Validators.required]],
      password: ['', [Validators.required]],
      rememberMe: [true],
    });
  }

  login() {
    this.submitted = true;
    if (this.loginForm.valid) {
      const loginUser: LoginUser = {
        Email: this.loginForm.value.email,
        Password: this.loginForm.value.password,
      };

      this.sharedService.showLoading(true);
      this.accountService
        .login(loginUser, this.loginForm.value.rememberMe)
        .subscribe({
          next: (res: any) => {
            this.sharedService.showLoading(false);
            this.router.navigateByUrl('/');
          },
          error: (err) => {
            this.sharedService.showLoading(false);
            // su ly email chua confirm

            if (err.error.Value.message === 'Please confirm your email.') {
              this.router.navigate(['/account/confirm-email'], {
                queryParams: { status: 'error' },
              });
            }  else if (err.error.Value.title === 'locked') {
              this.router.navigateByUrl('/account/locked');
              this.sharedService.passObj(err.error.Value.message);
            }
            else {
              this.errorMessage = err.error.Value.message;
            }
          },
        });
    }
  }
}

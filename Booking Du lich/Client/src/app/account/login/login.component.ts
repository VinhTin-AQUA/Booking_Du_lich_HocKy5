import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { AccountService } from '../account.service';
import { LoginUser } from 'src/app/shared/models/account/loginUser';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  loginForm: FormGroup = new FormGroup([]);
  rememberMe: boolean = true;
  submitted: boolean = false;

  constructor(
    private formBuilder: FormBuilder,
    private accountService: AccountService,
    private sharedService: SharedService,
    private router: Router
  ) {
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
        email: this.loginForm.value.email,
        password: this.loginForm.value.password,
      };

      this.sharedService.showLoading(true);
      this.accountService.login(loginUser, this.loginForm.value.rememberMe).subscribe({
        next: (res: any) => {
          this.sharedService.showLoading(false);
          this.router.navigateByUrl('/');
        },
        error: (err) => {
          this.sharedService.showLoading(false);

          console.log(err.error);
        },
      });
    }
  }
}

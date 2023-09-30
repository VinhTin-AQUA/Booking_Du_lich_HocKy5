import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { LoginUser } from 'src/app/shared/models/account/loginUser';

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
    private accountService: AccountService
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

      this.accountService.login(loginUser).subscribe({
        next: (res:any) => {
          console.log(res)
        },
        error: (err) => {
          console.log(err.error);
        }
      });
    }
  }
}

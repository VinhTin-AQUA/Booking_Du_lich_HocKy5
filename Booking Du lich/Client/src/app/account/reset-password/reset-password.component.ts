import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { ResetPassword } from 'src/app/shared/models/account/resetPassword';
import { AccountService } from '../account.service';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss'],
})
export class ResetPasswordComponent {
  formSubmit: FormGroup = new FormGroup([]);
  submitted: boolean = false;
  errorMessage: string = '';
  errorMessages: string[] = [];
  model: ResetPassword = {
    password: '',
    confirmPassword: '',
    token: '',
    email: '',
  };

  constructor(
    private activatedRoute: ActivatedRoute,
    private formBuilder: FormBuilder,
    private accountService: AccountService,
    private sharedService: SharedService,
    private router: Router
  ) {
    this.getTokenAndEmail();

    this.formSubmit = this.formBuilder.group({
      password: ['', [Validators.required]],
      confirmPassword: ['', [Validators.required]],
    });
  }

  private getTokenAndEmail() {
    this.activatedRoute.queryParams.subscribe({
      next: (params: any) => {
        this.model.token = params.token;
        this.model.email = params.email;
      },
    });
  }

  reset() {
    this.submitted = true;
    
    if (this.formSubmit.valid) {
      const password = this.formSubmit.value.password;

      const confirmPassword = this.formSubmit.value.confirmPassword;

      this.model.password = password;
      this.model.confirmPassword = confirmPassword;
      this.sharedService.showLoading(true);
      if (password !== confirmPassword) {
        this.errorMessage = 'Password is not match';
        this.sharedService.showLoading(false);
      } else {
        this.accountService.resetPassword(this.model).subscribe({
          next: (res: any) => {
            this.sharedService.showLoading(false);
            this.sharedService.showToastMessage('success' + res.value.message);
            this.router.navigateByUrl('/account/login');
          },
          error: (err) => {
            console.log(err.error.errors);
            this.errorMessages = err.error.errors
            this.sharedService.showLoading(false);
          },
        });
      }
    }
  }
}

import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { Router } from '@angular/router';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss'],
})
export class ForgotPasswordComponent {
  submitted: boolean = false;
  formSubmit: FormGroup = new FormGroup([]);
  errorMessage: string = '';

  constructor(
    private formBuilder: FormBuilder,
    private accountService: AccountService,
    private router: Router,
    private sharedService: SharedService
  ) {
    this.formSubmit = this.formBuilder.group({
      email: ['', [Validators.email, Validators.required]],
    });
  }

  send() {
    this.submitted = true;
    if (this.formSubmit.valid) {
      this.sharedService.showLoading(true);

      this.accountService
        .forgotPassword(this.formSubmit.value.email)
        .subscribe({
          next: (res: any) => {
            if (res.value.message === 'Email sent') {
              this.router.navigate(['/account/send-email-reset-password']);
            }
            this.sharedService.showLoading(false);
          },
          error: (err) => {
            this.sharedService.showLoading(false);
            this.errorMessage = err.error.value.message;
          },
        });
    }
  }
}

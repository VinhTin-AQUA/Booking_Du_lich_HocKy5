import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { AccountService } from '../account.service';
import { ResendEmail } from 'src/app/shared/models/account/resendEmail';
import { SharedService } from 'src/app/shared/shared.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-resend-email-confirm',
  templateUrl: './resend-email-confirm.component.html',
  styleUrls: ['./resend-email-confirm.component.scss'],
})
export class ResendEmailConfirmComponent {
  formSubmit: FormGroup = new FormGroup([]);
  submit: boolean = false;
  errorMessages: string = '';

  constructor(
    private accountService: AccountService,
    private formBuilder: FormBuilder,
    private sharedService: SharedService,
    private router: Router
  ) {
    this.formSubmit = formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
    });
  }

  send() {
    this.submit = true;
    if (this.formSubmit.valid) {
      this.sharedService.showLoading(true);
      const model: ResendEmail = { email: this.formSubmit.value.email };
      this.accountService.resendEmailConfirm(model).subscribe({
        next: (res:any) => {
          if (res.value.message === 'Your email has been confirmed.') {
            this.router.navigate(['/account/confirm-email'], {
              queryParams: { status: 'success' },
            });
          } else {
            this.router.navigateByUrl('/account/send-email-confirm');
          }
          this.sharedService.showLoading(false);
        },
        error: (err) => {
          this.errorMessages = err.error.value.message;
          this.sharedService.showLoading(false);
        },
      });
    }
  }
}

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { AccountService } from '../account.service';
import { ConfirmEmail } from 'src/app/shared/models/account/confirmEmail';

@Component({
  selector: 'app-confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrls: ['./confirm-email.component.scss'],
})
export class ConfirmEmailComponent implements OnInit {
  title: string = '';
  message: string = '';
  confirmed: boolean = true;

  constructor(
    private accountService: AccountService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.activatedRoute.queryParamMap.subscribe({
      next: (params: any) => {
        const status = params.get('status');

        if (status === 'success') {
          this.title = 'Your email has been confirmed';
          this.message = `Please login to use the app.`;
        } else if (status === 'error') {
          this.title = 'Your email does not confirm';
          this.message = `Please confirm your email to use our app`;
          this.confirmed = false;
        } else {
          const model: ConfirmEmail = {
            token: params.get('token'),
            email: params.get('email'),
          };
          this.accountService.confirmEmail(model).subscribe({
            next: (res: any) => {
              this.title = 'Thank you for trusting our service';
              this.message = 'Your email has been successfully confirmed. Please click the button below to log in';
            },
            error: (err: any) => {
              console.log(err.error);
            },
          });
        }
      },
    });
  }
}

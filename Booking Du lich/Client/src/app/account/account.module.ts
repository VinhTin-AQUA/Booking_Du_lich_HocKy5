import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

import { AccountRoutingModule } from './account-routing.module';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { SendEmailConfirmComponent } from './send-email-confirm/send-email-confirm.component';
import { ConfirmEmailComponent } from './confirm-email/confirm-email.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { ConfirmPasswordComponent } from './confirm-password/confirm-password.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { ResendEmailConfirmComponent } from './resend-email-confirm/resend-email-confirm.component';
import { SendEmailResetPasswordComponent } from './send-email-reset-password/send-email-reset-password.component';
import { LockoutEndComponent } from './lockout-end/lockout-end.component';
import { ProfileComponent } from './profile/profile.component';

@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent,
    SendEmailConfirmComponent,
    ConfirmEmailComponent,
    ForgotPasswordComponent,
    ConfirmPasswordComponent,
    ResetPasswordComponent,
    ResendEmailConfirmComponent,
    SendEmailResetPasswordComponent,
    LockoutEndComponent,
    ProfileComponent,
  ],
  imports: [
    CommonModule,
    AccountRoutingModule,
    ReactiveFormsModule,
    FormsModule,
  ],
})
export class AccountModule {}

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { SendEmailConfirmComponent } from './send-email-confirm/send-email-confirm.component';
import { ConfirmEmailComponent } from './confirm-email/confirm-email.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { ConfirmPasswordComponent } from './confirm-password/confirm-password.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { ResendEmailConfirmComponent } from './resend-email-confirm/resend-email-confirm.component';
import { SendEmailResetPasswordComponent } from './send-email-reset-password/send-email-reset-password.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent, title: 'Login' },
  { path: 'register', component: RegisterComponent, title: 'Register' },
  { path: 'send-email-confirm', component: SendEmailConfirmComponent, title: 'Register' },
  { path: 'confirm-email', component: ConfirmEmailComponent, title: 'Confirm email' },
  { path: 'forgot-password', component: ForgotPasswordComponent, title: 'Forgot password' },
  { path: 'reset-password', component: ResetPasswordComponent, title: 'Reset password' },
  { path: 'resend-email-confirm', component: ResendEmailConfirmComponent, title: 'Resend email confirm' },
  { path: 'send-email-reset-password', component: SendEmailResetPasswordComponent, title: 'Send email reset password' },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AccountRoutingModule {}

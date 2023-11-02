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
import { LockoutEndComponent } from './lockout-end/lockout-end.component';
import { ProfileComponent } from './profile/profile.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent, title: 'Đăng nhập' },
  { path: 'register', component: RegisterComponent, title: 'Đăng ký' },
  { path: 'send-email-confirm', component: SendEmailConfirmComponent, title: 'Gửi email xác thực' },
  { path: 'confirm-email', component: ConfirmEmailComponent, title: 'Xác thực email' },
  { path: 'forgot-password', component: ForgotPasswordComponent, title: 'Quên mật khẩu' },
  { path: 'reset-password', component: ResetPasswordComponent, title: 'Đặt lại mật khẩu' },
  { path: 'resend-email-confirm', component: ResendEmailConfirmComponent, title: 'Gửi email xác thực' },
  { path: 'send-email-reset-password', component: SendEmailResetPasswordComponent, title: 'Gửi email đặt lại mật khẩu' },
  { path: 'locked', component: LockoutEndComponent, title: 'Khóa' },
  { path: 'profile', component: ProfileComponent, title: 'Thông tin của bạn' },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AccountRoutingModule {}

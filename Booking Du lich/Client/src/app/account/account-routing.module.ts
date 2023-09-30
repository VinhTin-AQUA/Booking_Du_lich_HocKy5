import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { SendEmailConfirmComponent } from './send-email-confirm/send-email-confirm.component';
import { ConfirmEmailComponent } from './confirm-email/confirm-email.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent, title: 'Login' },
  { path: 'register', component: RegisterComponent, title: 'Register' },
  { path: 'send-email-confirm', component: SendEmailConfirmComponent, title: 'Register' },
  { path: 'confirm-email', component: ConfirmEmailComponent, title: 'Confirm email' },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AccountRoutingModule {}

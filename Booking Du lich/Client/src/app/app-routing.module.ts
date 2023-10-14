import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  { path: '', component: HomeComponent, title: 'Home' },
  {
    path: 'account',
    loadChildren: () =>
      import('../app/account/account.module').then((m) => m.AccountModule),
  },
  {
    path: 'city',
    loadChildren: () =>
      import('../app/booking/booking.module').then((m) => m.BookingModule),
  },
  {
    path: 'admin',
    loadChildren: () => import('../app/admin/admin.module').then((m) => m.AdminModule),
  },
  {
    path: 'agent',
    loadChildren: () => import('../app/agent/agent.module').then((m) => m.AgentModule),
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}

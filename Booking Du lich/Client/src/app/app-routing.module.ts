import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AccessDeniedComponent } from './access-denied/access-denied.component';
import { Roles } from './shared/guards/roles';
import { authGuard } from './shared/guards/auth.guard';


const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    title: 'Home',
    canActivate:[authGuard]
  },
  {
    path: 'access-denied',
    component: AccessDeniedComponent,
    title: 'Access denied',
  },
  {
    path: 'account',
    loadChildren: () =>
      import('../app/account/account.module').then((m) => m.AccountModule),
  },
  {
    path: 'city',
    loadChildren: () =>
      import('../app/booking/booking.module').then((m) => m.BookingModule),
      canActivate:[authGuard]
  },
  {
    path: 'admin',
    loadChildren: () =>
      import('../app/admin/admin.module').then((m) => m.AdminModule),
    canActivate: [authGuard],
    data: { role: Roles.ADMIN },
  },
  {
    path: 'agent',
    loadChildren: () =>
      import('../app/agent/agent.module').then((m) => m.AgentModule),
    canActivate: [authGuard],
    data: { role: Roles.AGENTHOTEL },
  },
  {
    path: 'agent-tour',
    loadChildren: () =>
      import('../app/agent-tour/agent-tour.module').then((m) => m.AgentTourModule),
    canActivate: [authGuard],
    data: { role: Roles.AGENTTOUR },
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}

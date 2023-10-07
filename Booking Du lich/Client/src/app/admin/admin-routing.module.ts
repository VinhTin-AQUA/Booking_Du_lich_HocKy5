import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProfileComponent } from './profile/profile.component';
import { AdminComponent } from './admin.component';
import { UserMangementComponent } from './user-mangement/user-mangement.component';
import { NotificationComponent } from './notification/notification.component';

const routes: Routes = [
  {path: '', component: AdminComponent, 
    children: [
      { path: 'profile', component: ProfileComponent, title: 'Profile' },
      { path: 'user-management', component: UserMangementComponent, title: 'User management' },
      { path: 'notification', component: NotificationComponent, title: 'User management' },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }

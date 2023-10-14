import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProfileComponent } from './profile/profile.component';
import { AdminComponent } from './admin.component';
import { UserMangementComponent } from './user-mangement/user-mangement.component';
import { NotificationComponent } from './notification/notification.component';
import { CityManangerComponent } from './city-mananger/city-mananger.component';
import { HotelManagerComponent } from './hotel-manager/hotel-manager.component';
import { HotelDetailComponent } from './hotel-detail/hotel-detail.component';

const routes: Routes = [
  {path: '', component: AdminComponent, 
    children: [
      { path: 'profile', component: ProfileComponent, title: 'Profile' },
      { path: 'user-management', component: UserMangementComponent, title: 'User management' },
      { path: 'notification', component: NotificationComponent, title: 'User management' },
      { path: 'city-management', component: CityManangerComponent, title: 'City management' },
      { path: 'hotel-management', component: HotelManagerComponent, title: 'Hotel management' },
      { path: 'hotel-detail/:id', component: HotelDetailComponent, title: 'Hotel detail' },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }

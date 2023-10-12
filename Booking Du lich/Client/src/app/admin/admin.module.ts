import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { FormsModule } from '@angular/forms';

import { AdminRoutingModule } from './admin-routing.module';
import { ProfileComponent } from './profile/profile.component';
import { AdminComponent } from './admin.component';
import { SideBarComponent } from './side-bar/side-bar.component';
import { UserMangementComponent } from './user-mangement/user-mangement.component';
import { NotificationComponent } from './notification/notification.component';
import { CityManangerComponent } from './city-mananger/city-mananger.component';
import { HotelManagerComponent } from './hotel-manager/hotel-manager.component';


@NgModule({
  declarations: [
    ProfileComponent,
    AdminComponent,
    SideBarComponent,
    UserMangementComponent,
    NotificationComponent,
    CityManangerComponent,
    HotelManagerComponent,
    
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    ReactiveFormsModule,
    MatIconModule,
    FormsModule
  ]
})
export class AdminModule { }

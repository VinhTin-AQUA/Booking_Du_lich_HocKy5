import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { FormsModule } from '@angular/forms';

import { AdminRoutingModule } from './admin-routing.module';
import { AdminComponent } from './admin.component';
import { SideBarComponent } from './side-bar/side-bar.component';
import { UserMangementComponent } from './user-mangement/user-mangement.component';
import { CityManangerComponent } from './city-mananger/city-mananger.component';
import { AddAccountComponent } from './add-account/add-account.component';
import { FeedBackComponent } from './feed-back/feed-back.component';
import { ManageTourTypeComponent } from './manage-tour-type/manage-tour-type.component';


@NgModule({
  declarations: [
    AdminComponent,
    SideBarComponent,
    UserMangementComponent,
    CityManangerComponent,
    AddAccountComponent,
    FeedBackComponent,
    ManageTourTypeComponent,
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

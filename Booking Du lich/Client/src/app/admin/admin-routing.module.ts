import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminComponent } from './admin.component';
import { UserMangementComponent } from './user-mangement/user-mangement.component';
import { CityManangerComponent } from './city-mananger/city-mananger.component';
import { AddAccountComponent } from './add-account/add-account.component';
import { FeedBackComponent } from './feed-back/feed-back.component';
import { ManageTourTypeComponent } from './manage-tour-type/manage-tour-type.component';

const routes: Routes = [
  {path: '', component: AdminComponent, 
    children: [
      { path: 'user-management', component: UserMangementComponent, title: 'User management' },
      { path: 'city-management', component: CityManangerComponent, title: 'City management' },
      { path: 'add-account', component: AddAccountComponent, title: 'Add account' },
      { path: 'manage-tour-type', component: ManageTourTypeComponent, title: 'Manage tour' },
      { path: 'feedback', component: FeedBackComponent, title: 'Feedback' },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }

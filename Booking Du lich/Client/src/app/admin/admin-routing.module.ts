import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminComponent } from './admin.component';
import { UserMangementComponent } from './user-mangement/user-mangement.component';
import { CityManangerComponent } from './city-mananger/city-mananger.component';
import { AddAccountComponent } from './add-account/add-account.component';
import { FeedBackComponent } from './feed-back/feed-back.component';
import { ManageTourTypeComponent } from './manage-tour-type/manage-tour-type.component';
import { BussinessPartnerComponent } from './bussiness-partner/bussiness-partner.component';
import { AddBussinessPartComponent } from './add-bussiness-part/add-bussiness-part.component';
import { ManageAgentTourComponent } from './manage-agent-tour/manage-agent-tour.component';

const routes: Routes = [
  {path: '', component: AdminComponent, 
    children: [
      { path: 'user-management', component: UserMangementComponent, title: 'User management' },
      { path: 'city-management', component: CityManangerComponent, title: 'City management' },
      { path: 'add-account', component: AddAccountComponent, title: 'Add account' },
      { path: 'manage-tour-type', component: ManageTourTypeComponent, title: 'Manage tour' },
      { path: 'feedback', component: FeedBackComponent, title: 'Feedback' },
      { path: 'bussiness-partner', component: BussinessPartnerComponent, title: 'Quản lý các đối tác' },
      { path: 'bussiness-partner/add-bussiness-partner', component: AddBussinessPartComponent, title: 'Thêm đối tác' },
      { path: 'manage-agent-tour', component: ManageAgentTourComponent, title: 'Quản lý đói tác tour' },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }

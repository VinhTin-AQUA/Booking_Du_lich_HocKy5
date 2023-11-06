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
      {path: '', redirectTo: 'user-managemen', pathMatch: 'full'},
      { path: 'user-management', component: UserMangementComponent, title: 'Quản lý người dùng' },
      { path: 'city-management', component: CityManangerComponent, title: 'Quản lý tỉnh thành phố' },

      { path: 'bussiness-partner/:id/add-account', component: AddAccountComponent, title: 'Cấp tài khoản' },
      { path: 'agent-tour/add-account', component: AddAccountComponent, title: 'Cấp tài khoản' },

      { path: 'manage-tour-type', component: ManageTourTypeComponent, title: 'Quản lý các loại tour' },
      { path: 'feedback', component: FeedBackComponent, title: 'Phản hồi' },
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

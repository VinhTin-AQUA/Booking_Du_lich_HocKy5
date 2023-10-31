import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AgentTourComponent } from './agent-tour.component';
import { AddTourComponent } from './add-tour/add-tour.component';
import { ManageTourComponent } from './manage-tour/manage-tour.component';
import { UpdateTourComponent } from './update-tour/update-tour.component';
import { AddTourTypeComponent } from './add-tour-type/add-tour-type.component';

const routes: Routes = [  {
  path: '',
  component: AgentTourComponent,
  children: [
    { path: 'add-tour', component: AddTourComponent, title: 'Tour', },
    { path: 'manage-tour', component: ManageTourComponent, title: 'Quản lý tour', },
    { path: 'update-tour', component: UpdateTourComponent, title: 'Cập nhật tour', },
    { path: ':id/add-tour-type', component: AddTourTypeComponent, title: 'Thêm loại tour', },
  ],
},];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AgentTourRoutingModule { }

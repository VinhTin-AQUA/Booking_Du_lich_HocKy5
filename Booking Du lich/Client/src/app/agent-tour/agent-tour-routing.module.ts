import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AgentTourComponent } from './agent-tour.component';
import { AddTourComponent } from './add-tour/add-tour.component';
import { ManagePackageComponent } from './manage-package/manage-package.component';
import { ManageTourComponent } from './manage-tour/manage-tour.component';
import { UpdateTourComponent } from './update-tour/update-tour.component';

const routes: Routes = [  {
  path: '',
  component: AgentTourComponent,
  children: [
    { path: 'add-tour', component: AddTourComponent, title: 'Tour', },
    { path: 'manage-package', component: ManagePackageComponent, title: 'Manage package', },
    { path: 'manage-tour', component: ManageTourComponent, title: 'Manage tour', },
    { path: 'update-tour', component: UpdateTourComponent, title: 'Update tour', },
  ],
},];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AgentTourRoutingModule { }

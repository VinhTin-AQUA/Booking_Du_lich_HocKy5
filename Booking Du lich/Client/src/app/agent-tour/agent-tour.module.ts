import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';

import { AgentTourRoutingModule } from './agent-tour-routing.module';
import { SidebarComponent } from './sidebar/sidebar.component';
import { AddTourComponent } from './add-tour/add-tour.component';
import { AgentTourComponent } from './agent-tour.component';
import { ManagePackageComponent } from './manage-package/manage-package.component';
import { ManageTourComponent } from './manage-tour/manage-tour.component';
import { UpdateTourComponent } from './update-tour/update-tour.component';


@NgModule({
  declarations: [
    SidebarComponent,
    AddTourComponent,
    AgentTourComponent,
    ManagePackageComponent,
    ManageTourComponent,
    UpdateTourComponent,
  ],
  imports: [
    CommonModule,
    AgentTourRoutingModule,
    MatIconModule
  ]
})
export class AgentTourModule { }

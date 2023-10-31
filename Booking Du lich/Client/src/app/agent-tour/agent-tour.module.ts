import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { ReactiveFormsModule } from '@angular/forms';

import { AgentTourRoutingModule } from './agent-tour-routing.module';
import { SidebarComponent } from './sidebar/sidebar.component';
import { AddTourComponent } from './add-tour/add-tour.component';
import { AgentTourComponent } from './agent-tour.component';
import { ManageTourComponent } from './manage-tour/manage-tour.component';
import { UpdateTourComponent } from './update-tour/update-tour.component';
import { AddTourTypeComponent } from './add-tour-type/add-tour-type.component';


@NgModule({
  declarations: [
    SidebarComponent,
    AddTourComponent,
    AgentTourComponent,
    ManageTourComponent,
    UpdateTourComponent,
    AddTourTypeComponent
  ],
  imports: [
    CommonModule,
    AgentTourRoutingModule,
    MatIconModule,
    ReactiveFormsModule
  ]
})
export class AgentTourModule { }

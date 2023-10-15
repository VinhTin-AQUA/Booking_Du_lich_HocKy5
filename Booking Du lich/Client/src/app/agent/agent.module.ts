import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { ReactiveFormsModule } from '@angular/forms';

import { AgentRoutingModule } from './agent-routing.module';
import { ProfileComponent } from './profile/profile.component';
import { AgentComponent } from './agent.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { HotelComponent } from './hotel/hotel.component';



@NgModule({
  declarations: [
    ProfileComponent,
    AgentComponent,
    SidebarComponent,
    HotelComponent
  ],
  imports: [
    CommonModule,
    AgentRoutingModule,
    MatIconModule,
    ReactiveFormsModule
  ]
})
export class AgentModule { }

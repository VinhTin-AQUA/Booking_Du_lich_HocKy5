import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';

import { AgentRoutingModule } from './agent-routing.module';
import { ProfileComponent } from './profile/profile.component';
import { AgentComponent } from './agent.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { HotelComponent } from './hotel/hotel.component';
import { RoomDashboardComponent } from './room-dashboard/room-dashboard.component';
import { ServiceComponent } from './service/service.component';
import { RoomDetailComponent } from './room-detail/room-detail.component';



@NgModule({
  declarations: [
    ProfileComponent,
    AgentComponent,
    SidebarComponent,
    HotelComponent,
    RoomDashboardComponent,
    ServiceComponent,
    RoomDetailComponent
  ],
  imports: [
    CommonModule,
    AgentRoutingModule,
    MatIconModule,
    ReactiveFormsModule,
    FormsModule
  ]
})
export class AgentModule { }

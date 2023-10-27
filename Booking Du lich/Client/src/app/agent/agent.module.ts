import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';

import { AgentRoutingModule } from './agent-routing.module';
import { AgentComponent } from './agent.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { HotelComponent } from './hotel/hotel.component';
import { RoomDashboardComponent } from './room-dashboard/room-dashboard.component';
import { ServiceComponent } from './service/service.component';
<<<<<<< HEAD
import { ManageRoomTypeComponent } from './manage-room-type/manage-room-type.component';
=======
import { RoomDetailComponent } from './room-detail/room-detail.component';


>>>>>>> fbf7a2fbe52bc67243c690eef5a99389b5d4f982

@NgModule({
  declarations: [
    AgentComponent,
    SidebarComponent,
    HotelComponent,
    RoomDashboardComponent,
    ServiceComponent,
<<<<<<< HEAD
    ManageRoomTypeComponent,
=======
    RoomDetailComponent
>>>>>>> fbf7a2fbe52bc67243c690eef5a99389b5d4f982
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

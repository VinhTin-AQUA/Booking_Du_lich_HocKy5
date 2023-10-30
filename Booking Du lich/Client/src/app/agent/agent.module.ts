import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';

import { AgentRoutingModule } from './agent-routing.module';
import { AgentComponent } from './agent.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { ServiceComponent } from './service/service.component';
import { ManageRoomTypeComponent } from './manage-room-type/manage-room-type.component';
import { BussinessInfoComponent } from './bussiness-info/bussiness-info.component';
import { PostComponent } from './post/post.component';
import { PostDetailComponent } from './post-detail/post-detail.component';
import { AddRoomComponent } from './add-room/add-room.component';
import { ManageRoomComponent } from './manage-room/manage-room.component';

@NgModule({
  declarations: [
    AgentComponent,
    SidebarComponent,
    ServiceComponent,
    ManageRoomTypeComponent,
    BussinessInfoComponent,
    PostComponent,
    PostDetailComponent,
    AddRoomComponent,
    ManageRoomComponent,
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

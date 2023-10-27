import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AgentComponent } from './agent.component';
import { HotelComponent } from './hotel/hotel.component';
import { RoomDashboardComponent } from './room-dashboard/room-dashboard.component';
import { ServiceComponent } from './service/service.component';
import { ManageRoomTypeComponent } from './manage-room-type/manage-room-type.component';


const routes: Routes = [
  {
    path: '',
    component: AgentComponent,
    children: [
      { path: 'hotel', component: HotelComponent, title: 'Thông tin khách sạn', },
      { path: 'room-dashboard', component: RoomDashboardComponent, title: 'Quản lý các phòng', },
      { path: 'service', component: ServiceComponent, title: 'Các dịch vụ của khách sạn', },
      { path: 'manage-room-type', component: ManageRoomTypeComponent, title: 'Quản lý loại phòng', },

    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AgentRoutingModule {}

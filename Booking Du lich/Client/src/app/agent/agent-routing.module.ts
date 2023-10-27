import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AgentComponent } from './agent.component';
import { HotelComponent } from './hotel/hotel.component';
import { RoomDashboardComponent } from './room-dashboard/room-dashboard.component';
<<<<<<< HEAD
import { ServiceComponent } from './service/service.component';
import { ManageRoomTypeComponent } from './manage-room-type/manage-room-type.component';
=======
import { RoomDetailComponent } from './room-detail/room-detail.component';
>>>>>>> fbf7a2fbe52bc67243c690eef5a99389b5d4f982

const routes: Routes = [
  {
    path: '',
    component: AgentComponent,
    children: [
<<<<<<< HEAD
      { path: 'hotel', component: HotelComponent, title: 'Thông tin khách sạn', },
      { path: 'room-dashboard', component: RoomDashboardComponent, title: 'Quản lý các phòng', },
      { path: 'service', component: ServiceComponent, title: 'Các dịch vụ của khách sạn', },
      { path: 'manage-room-type', component: ManageRoomTypeComponent, title: 'Quản lý loại phòng', },
=======
      { path: 'profile-agent', component: ProfileComponent, title: 'Profile Agent', },
      { path: 'hotel', component: HotelComponent, title: 'Hotel', },
      { path: 'room-dashboard', component: RoomDashboardComponent, title: 'Room dashboard', },
      { path: 'room-details', component: RoomDetailComponent, title: 'Room details', },
>>>>>>> fbf7a2fbe52bc67243c690eef5a99389b5d4f982
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AgentRoutingModule {}

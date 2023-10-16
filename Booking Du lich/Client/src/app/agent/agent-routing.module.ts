import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AgentComponent } from './agent.component';
import { ProfileComponent } from './profile/profile.component';
import { HotelComponent } from './hotel/hotel.component';
import { RoomDashboardComponent } from './room-dashboard/room-dashboard.component';

const routes: Routes = [
  {
    path: '',
    component: AgentComponent,
    children: [
      { path: 'profile-agent', component: ProfileComponent, title: 'Profile Agent', },
      { path: 'hotel', component: HotelComponent, title: 'Hotel', },
      { path: 'room-dashboard', component: RoomDashboardComponent, title: 'Room dashboard', },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AgentRoutingModule {}

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AgentComponent } from './agent.component';
import { ProfileComponent } from './profile/profile.component';
import { HotelComponent } from './hotel/hotel.component';

const routes: Routes = [
  {
    path: '',
    component: AgentComponent,
    children: [
      {
        path: 'profile-agent',
        component: ProfileComponent,
        title: 'Profile Agent',
      },
      {
        path: 'hotel',
        component: HotelComponent,
        title: 'Hotel',
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AgentRoutingModule {}

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AgentComponent } from './agent.component';
import { ServiceComponent } from './service/service.component';
import { ManageRoomTypeComponent } from './manage-room-type/manage-room-type.component';
import { BussinessInfoComponent } from './bussiness-info/bussiness-info.component';
import { PostComponent } from './post/post.component';
import { PostDetailComponent } from './post-detail/post-detail.component';


const routes: Routes = [
  {
    path: '',
    component: AgentComponent,
    children: [
      { path: 'service', component: ServiceComponent, title: 'Các dịch vụ của khách sạn', },
      { path: 'post', component: PostComponent, title: 'Quản lý chi nhánh', },
      { path: 'post-detail/:id', component: PostDetailComponent, title: 'Thông tin bài viết', },
      { path: 'add-post', component: PostDetailComponent, title: 'Thêm bài viết', },
      { path: 'manage-room-type', component: ManageRoomTypeComponent, title: 'Quản lý loại phòng', },
      { path: 'bussiness-info', component: BussinessInfoComponent, title: 'Thông tin khách sạn', },

    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AgentRoutingModule {}

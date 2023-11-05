import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmployeeComponent } from './employee.component';
import { PostCensorshipComponent } from './post-censorship/post-censorship.component';
import { PostContentComponent } from './post-content/post-content.component';

const routes: Routes = [{
  path: '', component: EmployeeComponent,
  children: [
    {path: '', component: PostCensorshipComponent, title: 'kiểm duyệt' },
    {path: 'post-content/:id', component: PostContentComponent, title: 'Nội dung' }
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmployeeRoutingModule { }

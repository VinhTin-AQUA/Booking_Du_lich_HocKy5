import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EmployeeRoutingModule } from './employee-routing.module';
import { PostContentComponent } from './post-content/post-content.component';
import { PostCensorshipComponent } from './post-censorship/post-censorship.component';


@NgModule({
  declarations: [
    PostContentComponent,
    PostCensorshipComponent
  ],
  imports: [
    CommonModule,
    EmployeeRoutingModule
  ]
})
export class EmployeeModule { }

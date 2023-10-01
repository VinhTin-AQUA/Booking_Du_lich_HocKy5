import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';

import { LoadingComponent } from './components/loading/loading.component';
import { ToastMessageComponent } from './components/toast-message/toast-message.component';


@NgModule({
  declarations: [LoadingComponent, ToastMessageComponent],
  imports: [
    CommonModule,MatIconModule
  ],
  exports: [LoadingComponent,ToastMessageComponent]
})
export class SharedModule { }

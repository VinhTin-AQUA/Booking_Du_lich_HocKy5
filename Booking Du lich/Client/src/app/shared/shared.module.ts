import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';

import { LoadingComponent } from './components/loading/loading.component';
import { ToastMessageComponent } from './components/toast-message/toast-message.component';
import { StarPipe } from './pipes/city/star.pipe';
import { NoStarPipe } from './pipes/city/no-star.pipe';
import { RolePipe } from './pipes/auth/role.pipe';

@NgModule({
  declarations: [LoadingComponent, ToastMessageComponent, StarPipe, NoStarPipe, RolePipe],
  imports: [CommonModule, MatIconModule],
  exports: [LoadingComponent, ToastMessageComponent,StarPipe,NoStarPipe,RolePipe],
})
export class SharedModule {}

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';

import { LoadingComponent } from './components/loading/loading.component';
import { ToastMessageComponent } from './components/toast-message/toast-message.component';
import { StarPipe } from './pipes/city/star.pipe';
import { NoStarPipe } from './pipes/city/no-star.pipe';

@NgModule({
  declarations: [LoadingComponent, ToastMessageComponent, StarPipe, NoStarPipe],
  imports: [CommonModule, MatIconModule],
  exports: [LoadingComponent, ToastMessageComponent,StarPipe,NoStarPipe],
})
export class SharedModule {}

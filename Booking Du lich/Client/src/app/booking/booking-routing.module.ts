import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CityComponent } from './city/city.component';
import { DetailComponent } from './detail/detail.component';
import { BookComponent } from './book/book.component';

const routes: Routes = [
  { path: ':cityName/:id', component: CityComponent },
  { path: ':cityName/:id/:hotelId/detail', component: DetailComponent, title: 'Xem chi tiết' },
  { path: 'book', component: BookComponent, title: 'Đặt phòng' },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class BookingRoutingModule {}

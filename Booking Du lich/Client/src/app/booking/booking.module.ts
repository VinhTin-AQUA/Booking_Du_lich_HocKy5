import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookingRoutingModule } from './booking-routing.module';


import { CityComponent } from './city/city.component';
import { DetailComponent } from './detail/detail.component';
import { BookComponent } from './book/book.component';


@NgModule({
  declarations: [CityComponent, DetailComponent, BookComponent],
  imports: [
    CommonModule,
    BookingRoutingModule
  ]
})
export class BookingModule { }

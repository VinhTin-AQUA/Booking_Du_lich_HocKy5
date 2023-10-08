import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookingRoutingModule } from './booking-routing.module';


import { CityComponent } from './city/city.component';


@NgModule({
  declarations: [CityComponent],
  imports: [
    CommonModule,
    BookingRoutingModule
  ]
})
export class BookingModule { }

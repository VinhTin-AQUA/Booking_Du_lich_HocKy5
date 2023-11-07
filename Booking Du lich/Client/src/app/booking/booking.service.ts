import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class BookingService {

  constructor(private http: HttpClient) { }


  getHotelsInCity(cityId: number) {
    return this.http.get(`${environment.appUrl}/hotel/get-hotels-in-city?cityId=${cityId}`);
  }

  getRoomsOfHotel(HotelId: number) {
    return this.http.get(`${environment.appUrl}/room/search-rooms-of-hotel?HotelId=${HotelId}`);
  }

}

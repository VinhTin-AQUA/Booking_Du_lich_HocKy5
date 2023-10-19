import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class AgentService {

  constructor(private http: HttpClient) { }

  getHotelOfAgent(agentId: string | undefined) {
    return this.http.get(`${environment.appUrl}/hotel/get-hotel-of-agent?agentId=${agentId}`);
  }

  updateHotel(formData: FormData) {
    return this.http.put(`${environment.appUrl}/hotel/update-hotel`,formData);
  }

  deleteImgHotel(url: string) {
    return this.http.delete(`${environment.appUrl}/hotel/delete-img-hotel?url=${url}`);
  }

  deleteAllImgHotel(hotelId: number | undefined) {
    return this.http.delete(`${environment.appUrl}/hotel/delete-all-img-hotel?hotelId=${hotelId}`);
  }

  getRooms() {
    return this.http.get(`${environment.appUrl}/room/get-all-rooms`);
  }

  addRoom(form: FormData) {
    return this.http.post(`${environment.appUrl}/room/add-room`,form);
  }

  getImagesOfRoom(roomId: number) {
    return this.http.get(`${environment.appUrl}/room/get-images-of-room?roomId=${roomId}`);
  }

  deleteImgRoom(url: string) {
    return this.http.delete(`${environment.appUrl}/room/delete-img-room?url=${url}`);
  }

  updateRoom(form: FormData) {
    return this.http.put(`${environment.appUrl}/room/update-room`,form);
  }
}

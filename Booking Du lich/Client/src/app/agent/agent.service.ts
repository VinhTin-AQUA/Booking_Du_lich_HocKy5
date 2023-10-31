import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class AgentService {
  constructor(private http: HttpClient) {}

  //hotel
  addHotel(formData: FormData) {
    return this.http.post(`${environment.appUrl}/hotel/add-hotel`, formData);
  }

  getHotelsOfAgentHotel(posterId: string | undefined) {
    return this.http.get(
      `${environment.appUrl}/hotel/get-hotels-of-agent?posterId=${posterId}`
    );
  }

  getHotelById(hotelId: number) {
    return this.http.get(
      `${environment.appUrl}/hotel/get-hotel-by-id?id=${hotelId}`
    );
  }

  updateHotel(formData: FormData) {
    return this.http.put(`${environment.appUrl}/hotel/update-hotel`, formData);
  }

  deleteImgHotel(url: string) {
    return this.http.delete(
      `${environment.appUrl}/hotel/delete-img-hotel?url=${url}`
    );
  }

  deleteAllImgHotel(hotelId: number | undefined) {
    return this.http.delete(
      `${environment.appUrl}/hotel/delete-all-img-hotel?hotelId=${hotelId}`
    );
  }

  deleteHotel(hotelId: number) {
    return this.http.delete(
      `${environment.appUrl}/hotel/delete-hotel?hotelId=${hotelId}`
    );
  }

  //room
  getRooms() {
    return this.http.get(`${environment.appUrl}/room/get-all-rooms`);
  }

  addRoom(form: FormData) {
    return this.http.post(`${environment.appUrl}/room/add-room`, form);
  }

  getImagesOfRoom(roomId: number) {
    return this.http.get(
      `${environment.appUrl}/room/get-images-of-room?roomId=${roomId}`
    );
  }

  deleteImgRoom(url: string) {
    return this.http.delete(
      `${environment.appUrl}/room/delete-img-room?url=${url}`
    );
  }

  updateRoom(form: FormData) {
    return this.http.put(`${environment.appUrl}/room/update-room`, form);
  }

  // service
  addNewService(serviceName: string) {
    return this.http.post(`${environment.appUrl}/service/add-service`, {
      ServiceName: serviceName,
    });
  }

  getAllServices() {
    return this.http.get(`${environment.appUrl}/service/get-all-services`);
  }

  getServicesOfHotel(hotelID: number) {
    return this.http.get(
      `${environment.appUrl}/hasservice/search-service-by-hotel?hotelID=${hotelID}`
    );
  }

  addServiceToHotel(form: FormData) {
    return this.http.post(
      `${environment.appUrl}/hasservice/add-has-service`,
      form
    );
  }

  deleteServiceHotel(hotelId: number, serviceId: number) {
    return this.http.delete(
      `${environment.appUrl}/hasservice/delete-has-service?hotelID=${hotelId}&serviceId=${serviceId}`
    );
  }
}

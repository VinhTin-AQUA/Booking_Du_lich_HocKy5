import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment } from 'src/environments/environment.development';
import { AddHotel } from '../shared/models/hotel/addHotel';
import { Agent } from '../shared/models/hotel/addAgent';

@Injectable({
  providedIn: 'root',
})
export class AdminService {
  constructor(private http: HttpClient) {}

  addCity(formData: FormData) {
    return this.http.post(`${environment.appUrl}/city/add-city`, formData);
  }

  getAllCities() {
    return this.http.get(`${environment.appUrl}/city/get-all-cities`);
  }

  deleteCity(id: number) {
    return this.http.delete(`${environment.appUrl}/city/delete-city?id=${id}`);
  }

  updateCity(formData: FormData) {
    return this.http.put(`${environment.appUrl}/city/update-city`, formData);
  }

  searchCities(searchString: string) {
    return this.http.get(
      `${environment.appUrl}/city/search-cities?searchString=${searchString}`
    );
  }

  // user manager
  getUsers(currentPage: number, pageSize: number) {
    return this.http.get(
      `${environment.appUrl}/usermanager/get-users?currentPage=${currentPage}&pageSize=${pageSize}`
    );
  }

  lockUser(email: string) {
    return this.http.put(
      `${environment.appUrl}/usermanager/lock-user?email=${email}`,
      {}
    );
  }

  unlockUser(email: string) {
    return this.http.put(
      `${environment.appUrl}/usermanager/un-lock-user?email=${email}`,
      {}
    );
  }

  deleteUser(email: string) {
    return this.http.delete(
      `${environment.appUrl}/usermanager/delete-user?email=${email}`
    );
  }

  addHotel(model: AddHotel) {
    return this.http.post(`${environment.appUrl}/hotel/add-hotel`, model);
  }

  getAllHotels() {
    return this.http.get(`${environment.appUrl}/hotel/get-all-hotels`);
  }

  getHotelById(id: string | null){
    return this.http.get(`${environment.appUrl}/hotel/get-hotel-by-id?id=${id}`);
  }

  addAgent(model: Agent) {
    return this.http.post(`${environment.appUrl}/hotel/add-agent`,model);
  }

  deleteHotel(hotelId: string | null) {
    return this.http.delete(`${environment.appUrl}/hotel/delete-hotel?hotelId=${hotelId}`);
  }
}

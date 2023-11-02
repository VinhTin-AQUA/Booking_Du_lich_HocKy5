import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment } from 'src/environments/environment.development';
import { AddHotel } from '../shared/models/hotel/addHotel';
import { Agent } from '../shared/models/hotel/addAgent';
import { AddTourType } from '../shared/models/tour/addTourType';
import { UpdateTourType } from '../shared/models/tour/updateTourType';

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
  getUsers(currentPage: number, pageSize: number, searchString: string) {
    return this.http.get(
      `${environment.appUrl}/usermanager/get-users?currentPage=${currentPage}&pageSize=${pageSize}&searchString=${searchString}`
    );
  }

  getUserById(userId: string |undefined) {
    return this.http.get(
      `${environment.appUrl}/usermanager/get-user-by-id?userId=${userId}`
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

  // tour type
  addTourType(addTourType: AddTourType) {
    return this.http.post(
      `${environment.appUrl}/TourType/add-tourtype`,addTourType
    );
  }

  getAllTours() {
    return this.http.get(
      `${environment.appUrl}/TourType/get-all-tourtypes`
    );
  }

  deleteTourType(tourTypeId: number) {
    return this.http.delete(
      `${environment.appUrl}/TourType/delete-tourtype?id=${tourTypeId}`
    );
  }

  updateTourType(tourType: UpdateTourType) {
    return this.http.put(
      `${environment.appUrl}/TourType/update-tourtype`,tourType
    );
  }
}

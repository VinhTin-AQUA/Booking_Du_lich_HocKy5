import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AddCitty } from '../shared/models/destionation/addCity';
import { environment } from 'src/environments/environment.development';

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
}

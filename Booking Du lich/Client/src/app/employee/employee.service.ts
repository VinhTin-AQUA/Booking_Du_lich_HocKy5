import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class EmployeeService {
  constructor(private http: HttpClient) {}

  hotelNotApproved() {
    return this.http.get(`${environment.appUrl}/hotel/hotels-not-approved`);
  }

  hotelApproved() {
    return this.http.get(`${environment.appUrl}/hotel/hotels-approved`);
  }

  approve(userId: string | null, hotelId: number) {
    return this.http.put(`${environment.appUrl}/hotel/approve?userId=${userId}&hotelId=${hotelId}`, {});
  }
}

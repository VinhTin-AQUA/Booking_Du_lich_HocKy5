import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class AgentTourService {
  constructor(private http: HttpClient) {}

  addTour(form: FormData) {
    return this.http.post(`${environment.appUrl}/tour/add-tour`, form);
  }

  getToursOfPoster(posterId: string | undefined) {
    return this.http.get(`${environment.appUrl}/tour/get-tours-of-poster?posterId=${posterId}`);
  }

  getTourById(tourId: number | null) {
    return this.http.get(`${environment.appUrl}/tour/get-tour-by-id?id=${tourId}`);
  }

  getAllTourTypes() {
    return this.http.get(`${environment.appUrl}/tourtype/get-all-tourtypes`);
  }

  addTypeToTour(tourTypeId: number | null, tourId: number | null) {
    return this.http.put(`${environment.appUrl}/tour/add-type-to-tour?tourTypeId=${tourTypeId}&tourId=${tourId}`, {});
  }

  addTourType(form: FormData) {
    return this.http.post(`${environment.appUrl}/tourtype/add-tourtype`,form);
  }

  deleteTour(tourId: number) {
    return this.http.delete(`${environment.appUrl}/tour/delete-tour?tourId=${tourId}`);
  }

  deleteImg(url: string, tourId: number){
    return this.http.delete(`${environment.appUrl}/tour/delete-img-tour?url=${url}&tourId=${tourId}`);
  }

  updateTour(form:FormData) {
    return this.http.put(`${environment.appUrl}/tour/update-tour`,form);
  }
}

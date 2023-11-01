import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Hotel } from 'src/app/shared/models/hotel/hotel';
import { SharedService } from 'src/app/shared/shared.service';
import { BookingService } from '../booking.service';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-city',
  templateUrl: './city.component.html',
  styleUrls: ['./city.component.scss'],
})
export class CityComponent {
  cityName: string | null = '';
  cityId: number = -1;
  private subcription: Subscription | null = null;

  hotels: Hotel[] = [];
  fileNames: string[] = [];
  imgBase = environment.imgUrl

  constructor(
    private activatedRoute: ActivatedRoute,
    private sharedService: SharedService,
    private router: Router,
    private bookService: BookingService
  ) {}

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((params) => {
      this.cityName = params['cityName'];
      this.cityId = params['id'];
    });

    this.getHotelsInCity();
  }

  private getHotelsInCity() {
    this.bookService.getHotelsInCity(this.cityId).subscribe({
      next:(hotels: any) => {
        this.hotels = hotels.hotels;
        this.fileNames = hotels.fileNames;
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  ngOnDestroy() {
    this.subcription?.unsubscribe();
  }
}

import { Component } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { environment } from 'src/environments/environment.development';

import { City } from '../shared/models/city/city';
import { Home } from '../shared/models/home/home';
import { SharedService } from '../shared/shared.service';
import { AdminService } from '../admin/admin.service';

const today = new Date();
const month = today.getMonth();
const year = today.getFullYear();

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent {
  range = new FormGroup({
    start: new FormControl<Date | null>(null),
    end: new FormControl<Date | null>(null),
  });
  _environment = environment;

  // city
  cities: City[] = [];
  tempCities: City[] = []
  isShowedAllCities: boolean = false;

  // Featured homes
  currentFeaturedIndex: number = 0;
  hcmCity: Home[] = [
    {
      Title: 'Infinity Pool Signature - With Pool&Netflix 1',
      Address: 'District 4,Ho Chi Minh City',
      ImgUrl:
        'https://pix8.agoda.net/hotelImages/10308447/-1/bcba17d349b319466c89e995d2bc3e03.jpg?ce=0',
      Price: 120,
      Vote: 3.5,
    },
    {
      Title: 'NTA Serviced Apartments',
      Address: 'District 1, Ho Chi Minh City',
      ImgUrl:
        'https://pix8.agoda.net/hotelImages/5684454/-1/07fd71826409454317e7be5159f38134.jpg?ca=9&ce=1',
      Price: 542,
      Vote: 4.5,
    },
    {
      Title: 'Cherry apartment - Thao Dien Centre',
      Address: 'District 2,Ho Chi Minh City',
      ImgUrl:
        'https://pix8.agoda.net/hotelImages/18588481/-1/3fd29837e57bfead7de5da6f0586d290.jpg?ca=16&ce=1',
      Price: 120,
      Vote: 3,
    },
    {
      Title: 'Ekomo Home',
      Address: 'District 1,Ho Chi Minh City',
      ImgUrl:
        'https://pix8.agoda.net/hotelImages/36087877/-1/5232f35748f1af5439d39426e1152f70.jpg?ce=0',
      Price: 120,
      Vote: 4,
    },
    {
      Title: 'LuxHomes Saigon - Vinhomes Central Park',
      Address: 'Bình Thạnh, Ho Chi Minh City',
      ImgUrl:
        'https://pix8.agoda.net/hotelImages/28445719/-1/72042261901c048af5863ea33acbc3b9.jpg?ca=23&ce=0',
      Price: 542,
      Vote: 5,
    },
    {
      Title: 'Lavis 18 Residence',
      Address: 'District 3,Ho Chi Minh City',
      ImgUrl:
        'https://pix8.agoda.net/hotelImages/10308447/-1/bcba17d349b319466c89e995d2bc3e03.jpg?ce=0',
      Price: 120,
      Vote: 5,
    },
    {
      Title: 'SAIGON 9 - Rivergate Residence Infinity Pool',
      Address: 'District 4,Ho Chi Minh City',
      ImgUrl:
        'https://q-xx.bstatic.com/xdata/images/hotel/max500/227327647.jpg?k=8027b8c607323e4dc203b3f414bed3f2aee863a7ad02aee04ab2239791541ac1&o=',
      Price: 120,
      Vote: 5,
    },
    {
      Title: 'Cozrum Homes - Graceful Corner',
      Address: 'Phú Nhuậ Ho Chi Minh City',
      ImgUrl:
        'https://pix8.agoda.net/hotelImages/32728246/-1/2cf94b49e93cef2e4624bfda87d11ba1.jpg?ce=0',
      Price: 120,
      Vote: 3,
    },
  ];

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private sharedService: SharedService,
    private adminService: AdminService
  ) {
    this.getAllCitites();
  }

  private getAllCitites() {
    this.adminService.getAllCities().subscribe({
      next: (res: any) => {
        this.cities = res;
        if(this.isShowedAllCities === false) {
          this.tempCities = this.cities.slice(0, 14);
        }
      },
    });
  }

  seeMore() {
    this.isShowedAllCities = !this.isShowedAllCities;
    if(this.isShowedAllCities === false) {
      this.tempCities = this.cities.slice(0, 14);
    } else {
      this.tempCities = this.cities;
    }
  }

  show() {
    console.log(JSON.stringify(this.range.value));
  }


  // destionation
  navigateToCity(city: City) {
    const cityUrl = `city/${city.Name}/${city.Id}`;
    this.router.navigate([cityUrl], { relativeTo: this.route });
    // Đặt title trên tab của trình duyệt
    document.title = city.Name;
  }

  /* featured home */
  searchFeaturedHome(index: number) {
    this.currentFeaturedIndex = index; // District 4, Ho Chi Minh City
  }
}

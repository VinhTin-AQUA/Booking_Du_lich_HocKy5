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
  
  //search
  rooms: number = 1;
  people: number = 1;
  isShowedDetailPeople: boolean = false;

  // city
  cities: City[] = [];
  tempCities: City[] = []
  isShowedAllCities: boolean = false;

  // Featured homes
  currentFeaturedIndex: number = 0;
  hcmCity: Home[] = [
    {
      title: 'Infinity Pool Signature - With Pool&Netflix 1',
      address: 'District 4,Ho Chi Minh City',
      imgUrl:
        'https://pix8.agoda.net/hotelImages/10308447/-1/bcba17d349b319466c89e995d2bc3e03.jpg?ce=0',
      price: 120,
      vote: 3.5,
    },
    {
      title: 'NTA Serviced Apartments',
      address: 'District 1, Ho Chi Minh City',
      imgUrl:
        'https://pix8.agoda.net/hotelImages/5684454/-1/07fd71826409454317e7be5159f38134.jpg?ca=9&ce=1',
      price: 542,
      vote: 4.5,
    },
    {
      title: 'Cherry apartment - Thao Dien Centre',
      address: 'District 2,Ho Chi Minh City',
      imgUrl:
        'https://pix8.agoda.net/hotelImages/18588481/-1/3fd29837e57bfead7de5da6f0586d290.jpg?ca=16&ce=1',
      price: 120,
      vote: 3,
    },
    {
      title: 'Ekomo Home',
      address: 'District 1,Ho Chi Minh City',
      imgUrl:
        'https://pix8.agoda.net/hotelImages/36087877/-1/5232f35748f1af5439d39426e1152f70.jpg?ce=0',
      price: 120,
      vote: 4,
    },
    {
      title: 'LuxHomes Saigon - Vinhomes Central Park',
      address: 'Bình Thạnh, Ho Chi Minh City',
      imgUrl:
        'https://pix8.agoda.net/hotelImages/28445719/-1/72042261901c048af5863ea33acbc3b9.jpg?ca=23&ce=0',
      price: 542,
      vote: 5,
    },
    {
      title: 'Lavis 18 Residence',
      address: 'District 3,Ho Chi Minh City',
      imgUrl:
        'https://pix8.agoda.net/hotelImages/10308447/-1/bcba17d349b319466c89e995d2bc3e03.jpg?ce=0',
      price: 120,
      vote: 5,
    },
    {
      title: 'SAIGON 9 - Rivergate Residence Infinity Pool',
      address: 'District 4,Ho Chi Minh City',
      imgUrl:
        'https://q-xx.bstatic.com/xdata/images/hotel/max500/227327647.jpg?k=8027b8c607323e4dc203b3f414bed3f2aee863a7ad02aee04ab2239791541ac1&o=',
      price: 120,
      vote: 5,
    },
    {
      title: 'Cozrum Homes - Graceful Corner',
      address: 'Phú Nhuậ Ho Chi Minh City',
      imgUrl:
        'https://pix8.agoda.net/hotelImages/32728246/-1/2cf94b49e93cef2e4624bfda87d11ba1.jpg?ce=0',
      price: 120,
      vote: 3,
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

  increaseRooms() {
    this.rooms++;
  }

  decreaseRooms() {
    this.rooms <= 1 ? (this.rooms = 1) : this.rooms--;
  }

  increasePeolple() {
    this.people++;
  }

  decreasePeolple() {
    this.people <= 1 ? (this.people = 1) : this.people--;
  }

  showDetailPeople() {
    this.isShowedDetailPeople = !this.isShowedDetailPeople;
  }

  // destionation
  navigateToCity(city: City) {
    const cityUrl = `city/${city.name}`;
    this.router.navigate([cityUrl], { relativeTo: this.route });
    this.sharedService.passObj(city);
    // Đặt title trên tab của trình duyệt
    document.title = 'Hello ' + city.name;
  }

  /* featured home */
  searchFeaturedHome(index: number) {
    this.currentFeaturedIndex = index; // District 4, Ho Chi Minh City
  }
}

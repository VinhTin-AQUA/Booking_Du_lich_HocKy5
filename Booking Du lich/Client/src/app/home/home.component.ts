import { Component } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';

import { City } from '../shared/models/destionation/city';
import { Home } from '../shared/models/destionation/home';
import { SharedService } from '../shared/shared.service';

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

  //search
  rooms: number = 1;
  people: number = 1;
  isShowedDetailPeople: boolean = false;

  // destinations
  destinations: City[] = [
    {
      id: 1,
      name: 'Hồ Chí Minh',
      imgUrl:
        'https://i.pinimg.com/564x/29/f5/09/29f509d8e523248908879a08dc85d6e6.jpg',
      accommodations: 122,
    },
    {
      id: 2,
      name: 'Ha Noi',
      imgUrl:
        'https://i.pinimg.com/564x/c2/c5/94/c2c5945fa9edae6777f17ceb34f4f01e.jpg',
      accommodations: 573,
    },
    {
      id: 3,
      name: 'Nha Trang',
      imgUrl:
        'https://i.pinimg.com/564x/0c/c3/0b/0cc30b1475ad64ad2551ac2fda3abd77.jpg',
      accommodations: 731,
    },
    {
      id: 4,
      name: 'Da nang',
      imgUrl:
        'https://i.pinimg.com/564x/ef/69/c5/ef69c56e319d6db337527303f3501ee1.jpg',
      accommodations: 122,
    },
    {
      id: 5,
      name: 'Vung Tau',
      imgUrl:
        'https://i.pinimg.com/564x/4b/3f/b9/4b3fb9fc016382fe6efd1b9632e1d8b2.jpg',
      accommodations: 100,
    },
    {
      id: 6,
      name: 'Da Lat',
      imgUrl:
        'https://i.pinimg.com/736x/f7/3f/dc/f73fdc67b3cde1e6ae5a5cd3eb57d2ca.jpg',
      accommodations: 90,
    },
    {
      id: 7,
      name: 'Hoi An',
      imgUrl:
        'https://i.pinimg.com/564x/16/fa/db/16fadb45df083835170ff06aa7b29d39.jpg',
      accommodations: 632,
    },
    {
      id: 8,
      name: 'Hue',
      imgUrl:
        'https://i.pinimg.com/564x/a0/de/fc/a0defccddc35233efe6e5a5b3ccbfe23.jpg',
      accommodations: 0,
    },
    {
      id: 9,
      name: 'Ca Mau',
      imgUrl:
        'https://i.pinimg.com/564x/34/15/01/3415016e7eabb06c915a64a10c3e25b3.jpg',
      accommodations: 0,
    },
    {
      id: 10,
      name: 'Dak Lak',
      imgUrl:
        'https://i.pinimg.com/564x/30/98/c7/3098c7d6e898382e8162426da2cf449f.jpg',
      accommodations: 120,
    },
  ];

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
    private sharedService: SharedService
  ) {
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

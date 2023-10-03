import { Component } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { City } from '../shared/models/destionation/city';

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

  destinations: City[] = [
    {
      name: 'Ho Chi Minh',
      imgUrl:
        'https://i.pinimg.com/564x/29/f5/09/29f509d8e523248908879a08dc85d6e6.jpg',
      accommodations: 122,
    },
    {
      name: 'Ha Noi',
      imgUrl:
        'https://i.pinimg.com/564x/c2/c5/94/c2c5945fa9edae6777f17ceb34f4f01e.jpg',
      accommodations: 573,
    },
    {
      name: 'Nha Trang',
      imgUrl:
        'https://i.pinimg.com/564x/0c/c3/0b/0cc30b1475ad64ad2551ac2fda3abd77.jpg',
      accommodations: 731,
    },
    {
      name: 'Da nang',
      imgUrl:
        'https://i.pinimg.com/564x/ef/69/c5/ef69c56e319d6db337527303f3501ee1.jpg',
      accommodations: 122,
    },
    {
      name: 'Vung Tau',
      imgUrl:
        'https://i.pinimg.com/564x/4b/3f/b9/4b3fb9fc016382fe6efd1b9632e1d8b2.jpg',
      accommodations: 100,
    },
    {
      name: 'Da Lat',
      imgUrl:
        'https://i.pinimg.com/736x/f7/3f/dc/f73fdc67b3cde1e6ae5a5cd3eb57d2ca.jpg',
      accommodations: 90,
    },
    {
      name: 'Hoi An',
      imgUrl:
        'https://i.pinimg.com/564x/16/fa/db/16fadb45df083835170ff06aa7b29d39.jpg',
      accommodations: 632,
    },
    {
      name: 'Hue',
      imgUrl:
        'https://i.pinimg.com/564x/a0/de/fc/a0defccddc35233efe6e5a5b3ccbfe23.jpg',
      accommodations: 0,
    },
    {
      name: 'Ca Mau',
      imgUrl:
        'https://i.pinimg.com/564x/34/15/01/3415016e7eabb06c915a64a10c3e25b3.jpg',
      accommodations: 0,
    },
    {
      name: 'Dak Lak',
      imgUrl:
        'https://i.pinimg.com/564x/30/98/c7/3098c7d6e898382e8162426da2cf449f.jpg',
      accommodations: 120,
    },
    {
      name: 'Ho Chi Minh',
      imgUrl:
        'https://i.pinimg.com/564x/29/f5/09/29f509d8e523248908879a08dc85d6e6.jpg',
      accommodations: 122,
    },
    {
      name: 'Ha Noi',
      imgUrl:
        'https://i.pinimg.com/564x/c2/c5/94/c2c5945fa9edae6777f17ceb34f4f01e.jpg',
      accommodations: 573,
    },
    {
      name: 'Nha Trang',
      imgUrl:
        'https://i.pinimg.com/564x/0c/c3/0b/0cc30b1475ad64ad2551ac2fda3abd77.jpg',
      accommodations: 731,
    },
    {
      name: 'Da nang',
      imgUrl:
        'https://i.pinimg.com/564x/ef/69/c5/ef69c56e319d6db337527303f3501ee1.jpg',
      accommodations: 122,
    },
    {
      name: 'Vung Tau',
      imgUrl:
        'https://i.pinimg.com/564x/4b/3f/b9/4b3fb9fc016382fe6efd1b9632e1d8b2.jpg',
      accommodations: 100,
    },
    {
      name: 'Da Lat',
      imgUrl:
        'https://i.pinimg.com/736x/f7/3f/dc/f73fdc67b3cde1e6ae5a5cd3eb57d2ca.jpg',
      accommodations: 90,
    },
    {
      name: 'Hoi An',
      imgUrl:
        'https://i.pinimg.com/564x/16/fa/db/16fadb45df083835170ff06aa7b29d39.jpg',
      accommodations: 632,
    },
    {
      name: 'Hue',
      imgUrl:
        'https://i.pinimg.com/564x/a0/de/fc/a0defccddc35233efe6e5a5b3ccbfe23.jpg',
      accommodations: 0,
    },
    {
      name: 'Ca Mau',
      imgUrl:
        'https://i.pinimg.com/564x/34/15/01/3415016e7eabb06c915a64a10c3e25b3.jpg',
      accommodations: 0,
    },
    {
      name: 'Dak Lak',
      imgUrl:
        'https://i.pinimg.com/564x/30/98/c7/3098c7d6e898382e8162426da2cf449f.jpg',
      accommodations: 120,
    },
  ];

  rooms: number = 1;
  people: number = 1;
  isShowedDetailPeople: boolean = false;

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




}

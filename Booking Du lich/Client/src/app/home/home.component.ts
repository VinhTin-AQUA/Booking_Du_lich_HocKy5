import { Component } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';

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
}

import { Component } from '@angular/core';
import { AccountService } from 'src/app/account/account.service';

@Component({
  selector: 'app-hotels',
  templateUrl: './hotel.component.html',
  styleUrls: ['./hotel.component.scss']
})
export class HotelComponent {
  constructor(private accountService: AccountService) {
    this.accountService.user$.subscribe(user => {
      console.log(user);
      
    })
  }
}

import { Component, OnInit } from '@angular/core';
import { AccountService } from './account/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Booking App';

  constructor(private accountService: AccountService) {

  }

  ngOnInit(): void {
    this.refreshUser()
  }

  private refreshUser() {
    const jwt = this.accountService.getJwtUser();  
    if(jwt) {
      this.accountService.refreshUser(jwt).subscribe({
        next: _ => {
        },
        error: (err: any) => {
          //this.accountService.logout();
          this.accountService.notLogin();
        }
      })
    } else {
      this.accountService.refreshUser(null).subscribe();
    }
  }
}

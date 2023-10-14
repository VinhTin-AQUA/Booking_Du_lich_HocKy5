import { Component, OnInit } from '@angular/core';
import { AccountService } from './account/account.service';
import { Router, NavigationEnd } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'Booking App';

  constructor(private accountService: AccountService, private router: Router) {
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        //scroll to top
        window.scrollTo(0, 0);
      }
    });
  }

  ngOnInit(): void {
    this.refreshUser();
  }

  private refreshUser() {
    const jwt = this.accountService.getJwtUser();
    if (jwt) {
      this.accountService.refreshUser(jwt).subscribe({
        next: (_) => {},
        error: (err: any) => {
          //this.accountService.logout();
          this.accountService.notLogin();
          this.router.navigateByUrl('');
        },
      });
    } else {
      this.accountService.refreshUser(null).subscribe();
      this.router.navigateByUrl('');
    }
  }
}

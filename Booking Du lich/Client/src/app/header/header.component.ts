import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { SharedService } from '../shared/shared.service';
import { AccountService } from '../account/account.service';


@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent {
  collapseMenu: boolean = false;

  constructor(
    private sharedService: SharedService,
    public accountService: AccountService,
    private router: Router
  ) {
  }

  showMenu() {
    this.collapseMenu = !this.collapseMenu;
  }

  hideMenu() {
    this.collapseMenu = false;
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/account/login')
    this.hideMenu();
  }
}

import { Component } from '@angular/core';

import { SharedService } from '../shared/shared.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent {
  collapseMenu: boolean = false;

  constructor() {}

  showMenu() {
    this.collapseMenu = !this.collapseMenu;
  }

  hideMenu(){
    this.collapseMenu = false;
  }



  

}

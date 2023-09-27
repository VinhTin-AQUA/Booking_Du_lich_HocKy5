import { Component } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent {
  collapseMenu: boolean = false;

  showMenu() {
    this.collapseMenu = !this.collapseMenu;
  }

<<<<<<< HEAD
  hideMenu() {
=======
  hideMenu(){
>>>>>>> d20f87f637e0698958744b8d15f39bde110bd5a3
    this.collapseMenu = false;
  }
}

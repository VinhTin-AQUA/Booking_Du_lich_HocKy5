import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-manage-room',
  templateUrl: './manage-room.component.html',
  styleUrls: ['./manage-room.component.scss']
})
export class ManageRoomComponent {
  isShowDeleted: boolean = false;
  hotelId: number | null = null;


  constructor(private activatedRoute: ActivatedRoute) {

  }

  ngOnInit() {
    this.activatedRoute.params.subscribe({
      next: (params: any) => {
        if (params['id'] !== undefined) {
          this.hotelId = params['id'];
        }
        console.log(this.hotelId);
        
        //this.resetHotelForm();
      },
    });
  }

  showDelete() {
    this.isShowDeleted = !this.isShowDeleted;
  }
}

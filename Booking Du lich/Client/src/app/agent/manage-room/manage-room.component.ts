import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BookingService } from 'src/app/booking/booking.service';
import { Room } from 'src/app/shared/models/room/room';
import { AgentService } from '../agent.service';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-manage-room',
  templateUrl: './manage-room.component.html',
  styleUrls: ['./manage-room.component.scss'],
})
export class ManageRoomComponent {
  isShowDeleted: boolean = false;
  hotelId: number | null = null;
  rooms: Room[] = [];
  roomIdDelete: number | null = null;

  constructor(
    private activatedRoute: ActivatedRoute,
    private bookingService: BookingService,
    private agentService: AgentService,
    private sharedService: SharedService
  ) {}

  ngOnInit() {
    this.activatedRoute.params.subscribe({
      next: (params: any) => {
        if (params['id'] !== undefined) {
          this.hotelId = params['id'];
        }
        this.getRooms();
        //this.resetHotelForm();
      },
    });
  }

  private getRooms() {
    if (this.hotelId !== null) {
      this.bookingService
        .getRoomsOfHotel(this.hotelId)
        .subscribe((rooms: any) => {
          this.rooms = rooms.Value.Rooms;
        });
    }
  }

  showDelete(roomId: number | null) {
    this.isShowDeleted = !this.isShowDeleted;
    this.roomIdDelete = roomId;
  }

  resetRooms() {
    const index = this.rooms.findIndex(r => r.Id === this.roomIdDelete)
    if(index !== -1) {
      this.rooms.splice(index,1)
    }
    this.roomIdDelete = null;
  }

  deleteSubmit() {
    this.agentService.deleteRoom(this.roomIdDelete).subscribe({
      next: (_) => {
        this.sharedService.showToastMessage('successXóa thành công');

      },
      error: (err) => {
        console.log(err);
        this.sharedService.showToastMessage('Có lỗi khi xóa. Hãy thử lại')
      }
    })
  }
}

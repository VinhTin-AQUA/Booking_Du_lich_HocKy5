import { Component, ElementRef, ViewChild } from '@angular/core';
import { Room } from 'src/app/shared/models/room/room';
import { AgentService } from '../agent.service';
import { ImgShow } from 'src/app/shared/models/image/imgShow';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SharedService } from 'src/app/shared/shared.service';
import { AccountService } from 'src/app/account/account.service';
import { map, mergeMap } from 'rxjs';

@Component({
  selector: 'app-room-dashboard',
  templateUrl: './room-dashboard.component.html',
  styleUrls: ['./room-dashboard.component.scss'],
})
export class RoomDashboardComponent {
  searchString: string = '';
  rooms: Room[] = [];
  isShowedAddRoomModel: boolean = true;
  hotelId: string | undefined = '';
  userId: string | undefined = '';

  // img
  newImgObjToAdd: any = []; // object ảnh mới để tải lên server
  listNewImgUrls: ImgShow[] = []; // data ảnh mới để render view
  @ViewChild('fileinput') fileInput!: ElementRef;

  submitted: boolean = false;
  errorMessage: string[] = [];
  formRoomSubmit: FormGroup = new FormGroup([]);

  constructor(
    private agentService: AgentService,
    private formBuilder: FormBuilder,
    private sharedService: SharedService,
    private accountService: AccountService
  ) {
    this.formRoomSubmit = this.formBuilder.group({
      roomNumber: ['', [Validators.required]],
      name: ['', [Validators.required]],
      description: ['', [Validators.required]],
      isAvailable: [true],
    });

    this.accountService.user$
    .pipe(
      map((user) => {
        this.userId = user?.Id;
        return user;
      }),
      mergeMap((user) => this.agentService.getHotelOfAgent(user?.Id))
    )
    .subscribe({
      next: (res: any) => {
        //this.hotel = res.hotel;

        this.hotelId = res.hotel.Id;

        //for (let i of res.imgFileNames) {
        //  const temp = i.split('\\');

        //  const imgShow: ImgShow = {
        //    name: temp[temp.length - 1],
        //    data: i,
        //  };
        //  console.log(imgShow);

        //  this.imgFiles.push(imgShow);
        //}

        //if (this.hotel !== null && this.hotel.City !== null) {
        //  this.cityOfHotel = this.hotel.City;
        //}
        //this.formInit();
      },
      error: (err) => {
        console.log(err);
      },
    });


  }

  ngOnInit() {
    this.agentService.getRooms().subscribe({
      next: (res) => {
        console.log(res);
      },
    });
  }

  onSelectImage(event: any) {
    for (let file of event.target.files) {
      this.newImgObjToAdd.push(file);
    }

    if (event.target.files) {
      const n = event.target.files.length;
      for (let i = 0; i < n; i++) {
        var reader = new FileReader();
        reader.readAsDataURL(event.target.files[i]);

        let imgShow: ImgShow = {
          name: event.target.files[i].name,
          data: '',
        };

        reader.onload = (events: any) => {
          imgShow.data = events.target.result;
          this.listNewImgUrls.push(imgShow);
        };
      }
      this.clearFile();
    }
  }

  private clearFile() {
    this.fileInput.nativeElement.value = '';
  }

  removeImgUnSave(url: any) {
    const index = this.listNewImgUrls.findIndex((u: any) => u.data === url);
    if (index !== -1) {
      this.listNewImgUrls.splice(index, 1);
    }
  }

  getImagesHotel() {}

  //removeImgSaved(url: string) {
  //  console.log(url);

  //  this.agentService.deleteImgHotel(url).subscribe({
  //    next: (_) => {
  //      const index = this.imgFiles.findIndex((u: any) => u === url);
  //      if (index !== -1) {
  //        this.imgFiles.splice(index, 1);
  //      }
  //      this.sharedService.showToastMessage(
  //        'success Delete image successfully'
  //      );
  //    },
  //    error: (_) => {
  //      this.sharedService.showToastMessage('Please try again');
  //    },
  //  });
  //}

  submitAdd() {
    this.submitted = true;
    this.errorMessage = [];

    if (this.formRoomSubmit.valid) {
      let formData = new FormData();

      formData.append('RoomNumber', this.formRoomSubmit.value.roomNumber);
      formData.append('Name', this.formRoomSubmit.value.name);
      formData.append('Description', this.formRoomSubmit.value.description);
      formData.append('IsAvailable', this.formRoomSubmit.value.isAvailable);
      if(this.hotelId !== undefined) {
        formData.append('HotelId', this.hotelId);
      }

      for (let file of this.newImgObjToAdd) {
        formData.append('files', file);
      }
      this.sharedService.showLoading(true);
      
      this.agentService.addRoom(formData).subscribe({
        next: (res: any) => {
          this.getImagesHotel();
          this.sharedService.showToastMessage('success' + res.Value.message);
          this.sharedService.showLoading(false);
        },
        error: (err) => {
          this.sharedService.showToastMessage(err.error.Value.message);
          this.sharedService.showLoading(false);
        },
      });
    }
  }

  showAddRoomModal() {
    this.isShowedAddRoomModel = !this.isShowedAddRoomModel;
  }

  searchCities() {}
}

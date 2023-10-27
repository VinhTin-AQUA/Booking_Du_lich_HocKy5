import { Component, ElementRef, ViewChild } from '@angular/core';
import { Room } from 'src/app/shared/models/room/room';
import { AgentService } from '../agent.service';
import { ImgShow } from 'src/app/shared/models/image/imgShow';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SharedService } from 'src/app/shared/shared.service';
import { AccountService } from 'src/app/account/account.service';
import { map, mergeMap } from 'rxjs';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-room-dashboard',
  templateUrl: './room-dashboard.component.html',
  styleUrls: ['./room-dashboard.component.scss'],
})
export class RoomDashboardComponent {
  searchString: string = '';
  rooms: Room[] = [];
  firstImages: string[] = [];

  isShowedAddRoomModel: boolean = false;

  hotelId: string | undefined = '';
  userId: string | undefined = '';

  baseImgUrl = environment.imgUrl;

  // img
  newImgObjToAdd: any = []; // object ảnh mới để tải lên server
  listNewImgUrls: ImgShow[] = []; // data ảnh mới để render view
  listSavedImgUrl: ImgShow[] = []; // danh sách đường dẫn ảnh đã lưu trước đó của room
  @ViewChild('fileinput') fileInput!: ElementRef;

  submitted: boolean = false;
  errorMessage: string[] = [];
  formRoomSubmit: FormGroup = new FormGroup([]);
  isUpdate: boolean = false;
  roomIdUpdate: number = -1;

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
      price: [0, [Validators.required, Validators.min(0)]],
      validFrom: [Date.now,[Validators.required]],
      goodThru: [Date.now,[Validators.required]],
    });

    this.sharedService.showLoading(true);
    //this.accountService.user$
    //  .pipe(
    //    map((user) => {
    //      this.userId = user?.Id;
    //      return user;
    //    }),
    //    mergeMap((user) => this.agentService.getHotelOfAgent(user?.Id))
    //  )
    //  .subscribe({
    //    next: (res: any) => {
    //      this.hotelId = res.hotel.Id;
    //      this.sharedService.showLoading(false);
    //    },
    //    error: (err) => {
    //      console.log(err);
    //      this.sharedService.showLoading(false);
    //    },
    //  });
  }

  ngOnInit() {
    this.sharedService.showLoading(true);
    this.agentService.getRooms().subscribe({
      next: (res: any) => {
        this.rooms = res.rooms;
        if (res.firstImages[0] !== '') {
          this.firstImages = res.firstImages;
        }
        this.sharedService.showLoading(false);
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

  private getImagesOfRoom() {
    this.agentService.getImagesOfRoom(this.roomIdUpdate).subscribe({
      next: (res: any) => {
        for (let i of res) {
          const temp = i.split('\\');
          const imgShow: ImgShow = {
            name: temp[temp.length - 1],
            data: i,
          };
          this.listSavedImgUrl.push(imgShow);
        }
        this.listNewImgUrls = [];
      },
      error: (_) => {
        this.sharedService.showLoading(false);
      },
    });
  }

  private clearFile() {
    this.fileInput.nativeElement.value = '';
  }

  private clearForm() {
    this.formRoomSubmit.controls['roomNumber'].setValue('');
    this.formRoomSubmit.controls['name'].setValue('');
    this.formRoomSubmit.controls['description'].setValue('');
    this.formRoomSubmit.controls['isAvailable'].setValue(true);

    this.listNewImgUrls = [];
    this.newImgObjToAdd = [];
    this.listSavedImgUrl = [];
    this.isUpdate = false;
    this.roomIdUpdate = -1;
  }

  private initRoomUpdate(room: Room) {
    this.formRoomSubmit.controls['roomNumber'].setValue(room.RoomNumber);
    this.formRoomSubmit.controls['name'].setValue(room.Name);
    this.formRoomSubmit.controls['description'].setValue(room.Description);
    this.formRoomSubmit.controls['isAvailable'].setValue(room.IsAvailable);
  }

  removeImgUnSave(url: any) {
    const index = this.listNewImgUrls.findIndex((u: any) => u.data === url);
    if (index !== -1) {
      this.listNewImgUrls.splice(index, 1);
    }
  }

  removeImgSaved(url: string) {
    this.sharedService.showLoading(true);
    this.agentService.deleteImgRoom(url).subscribe({
      next: (_) => {
        const index = this.listSavedImgUrl.findIndex(
          (u: any) => u.data === url
        );
        if (index !== -1) {
          this.listSavedImgUrl.splice(index, 1);
        }
        this.sharedService.showToastMessage(
          'success Delete image successfully'
        );
        this.sharedService.showLoading(false);
      },
      error: (_) => {
        this.sharedService.showToastMessage('Please try again');
        this.sharedService.showLoading(false);
      },
    });
  }

  submit() {
    this.submitted = true;
    this.errorMessage = [];

    if (this.formRoomSubmit.valid) {
      let formData = new FormData();

      formData.append('RoomNumber', this.formRoomSubmit.value.roomNumber);
      formData.append('Name', this.formRoomSubmit.value.name);
      formData.append('Description', this.formRoomSubmit.value.description);
      formData.append('IsAvailable', this.formRoomSubmit.value.isAvailable);
      formData.append('Price', this.formRoomSubmit.value.price);
      formData.append('ValidFrom', this.formRoomSubmit.value.validFrom);
      formData.append('GoodThru', this.formRoomSubmit.value.goodThru);
      

      if (this.hotelId !== undefined) {
        formData.append('HotelId', this.hotelId);
      }

      for (let file of this.newImgObjToAdd) {
        formData.append('files', file);
      }
      this.sharedService.showLoading(true);

      // add room
      if(this.isUpdate === false) {
        this.agentService.addRoom(formData).subscribe({
          next: (res: any) => {
            this.sharedService.showToastMessage('success' + res.Value.message);
            this.sharedService.showLoading(false);
          },
          error: (err) => {
            this.sharedService.showToastMessage(err.error.Value.message);
            this.sharedService.showLoading(false);
          },
        });
      } else { // cap nhat room
        formData.append('Id', this.roomIdUpdate.toString());
        this.agentService.updateRoom(formData).subscribe({
          next: (res: any) => {
            this.getImagesOfRoom();
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
  }

  showAddRoomModal() {
    this.isShowedAddRoomModel = !this.isShowedAddRoomModel;
    if (this.isShowedAddRoomModel === false) {
      this.clearForm();
    }
  }

  showUpdateRoom(room: Room) {
    this.roomIdUpdate = room.Id;
    this.initRoomUpdate(room);
    this.getImagesOfRoom();
    this.showAddRoomModal();
    this.isUpdate = true;
  }
}

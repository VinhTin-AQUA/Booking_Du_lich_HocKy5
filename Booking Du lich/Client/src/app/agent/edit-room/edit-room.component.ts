import { Component, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { map, mergeMap } from 'rxjs'


import { Hotel } from 'src/app/shared/models/hotel/hotel';
import { ImgShow } from 'src/app/shared/models/image/imgShow';
import { environment } from 'src/environments/environment.development';
import { AgentService } from '../agent.service';
import { Room } from 'src/app/shared/models/room/room';
import { SharedService } from 'src/app/shared/shared.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RoomType } from 'src/app/shared/models/room/roomType';

@Component({
  selector: 'app-edit-room',
  templateUrl: './edit-room.component.html',
  styleUrls: ['./edit-room.component.scss']
})
export class EditRoomComponent {
  room: Room | null = null;
  envImgUrl = environment.imgUrl

  listNewImgUrls: ImgShow[] = []; // danh sách ảnh mới để hiển thị trên view
  newImgObjToAdd: any = []; // danh sách các đối tượng file gửi lên server
  imgFiles: ImgShow[] =[]

  @ViewChild('fileInput') fileInput!: ElementRef;

  submitted: boolean = false;
  updateForm: FormGroup = new FormGroup([])
  roomTypes: RoomType[]=[]



  constructor(private activatedRoute: ActivatedRoute,
    private agentService: AgentService,
    private sharedService: SharedService,
    private formBuilder: FormBuilder) {

  }

  ngOnInit() {
    this.updateForm = this.formBuilder.group({
      roomNumber: ['', [Validators.required]],
      name: ['', [Validators.required]],
      description: ['', [Validators.required]],
      isAvailable: ['', [Validators.required]],
      roomTypeId: ['', [Validators.required]],
      price: ['', [Validators.required]],
      validFrom: ['', [Validators.required]],
      goodThru: ['', [Validators.required]],
    })

    this.activatedRoute.params
      .pipe(
        map((params: any) => {
          return params;
        }),
        mergeMap((params) => this.agentService.getRoomById(params['id']))
      )
      .subscribe({
        next: (res: any) => {
          this.room = res.room;
          for (let img of res.imgNames) {
            const arr = img.split('\\');
            const imgName: string = arr[arr.length - 1];

            const imgShow: ImgShow = {
              name: imgName,
              data: img,
            };
            this.imgFiles.push(imgShow);
          }
          this.resetHotelForm();
        },
      });

    this.getAllRoomRypes();
  }

  private getAllRoomRypes() {
    this.agentService.getAllRoomType().subscribe((rt: any) => {
      this.roomTypes = rt.Value.RoomTypes;
    });
  }

  private clearFile() {
    this.fileInput.nativeElement.value = '';
  }

  private resetHotelForm() {
    if (this.room !== null) {
      this.updateForm.controls['roomNumber'].setValue(this.room.RoomNumber);
      this.updateForm.controls['name'].setValue(this.room.RoomName);
      this.updateForm.controls['description'].setValue(this.room.Description);
      this.updateForm.controls['isAvailable'].setValue(this.room.IsAvailable);
      this.updateForm.controls['roomTypeId'].setValue(this.room.RoomType.RoomTypeId);
      this.updateForm.controls['price'].setValue(this.room.RoomPrice.Price);
      this.updateForm.controls['validFrom'].setValue(this.room.RoomPrice.ValidFrom);
      this.updateForm.controls['goodThru'].setValue(this.room.RoomPrice.GoodThru);

      //this.agentService.getHotelById(this.hotelID).subscribe({
      //  next: (res: any) => {
      //    this.updateForm.controls['id'].setValue(res.hotel.Id);
      //    this.hotelGroup.controls['hotelName'].setValue(res.hotel.HotelName);
      //    this.hotelGroup.controls['address'].setValue(res.hotel.Address);
      //    this.hotelGroup.controls['description'].setValue(
      //      res.hotel.Description
      //    );
      //    this.cityId = res.hotel.CityId;
      //    this.imgNames = res.imgNames;
      //    this.postingDate = res.hotel.PostingDate;

      //    for (let img of res.imgNames) {
      //      const arr = img.split('\\');
      //      const imgName: string = arr[arr.length - 1];

      //      const imgShow: ImgShow = {
      //        name: imgName,
      //        data: img,
      //      };
      //      this.imgFiles.push(imgShow);
      //    }
      //  },
      //  error: (err) => {
      //    console.log(err);
      //  },
      //});
    }
  }

  removeImgSaved(data: string) {
    this.sharedService.showLoading(true);
    this.agentService.deleteImgHotel(data).subscribe({
      next: (res) => {
        this.sharedService.showToastMessage('successXóa thành công');
        this.sharedService.showLoading(false);
        const index = this.imgFiles.findIndex((url) => url.data === data);
        if (index !== -1) {
          this.imgFiles.splice(index, 1);
        }
      },
      error: (err) => {
        console.log(err);
        this.sharedService.showLoading(false);
      },
    });
  }

  removeImgUnSave(data: string) {
    const index = this.listNewImgUrls.findIndex((url) => url.data === data);
    if (index !== -1) {

      const fileName = this.listNewImgUrls[index].name;
      const _index = this.newImgObjToAdd.findIndex(
        (u: any) => u.name === fileName
      );
      this.listNewImgUrls.splice(index, 1);
      this.newImgObjToAdd.splice(_index, 1);
    }
  }

  onSelectImg(event: any) {
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

  deleteAllImg() {
    if (this.room !== null) {
      this.agentService.deleteAllImgRoom(this.room.Id).subscribe({
        next: (_) => {
          this.sharedService.showToastMessage(
            'success Delete all images successfully'
          );
          this.imgFiles = [];
          this.listNewImgUrls = [];
          this.newImgObjToAdd = [];
        },
        error: (_) => {
          this.sharedService.showToastMessage('Please try again');
        },
      });
    }
  }

  submit() {
    this.submitted = true;
  
    if (this.room !== null) {
      this.updateHotel();
    }
  }

  private updateHotel() {
    if (this.updateForm.valid && this.room !== null) {
      this.sharedService.showLoading(true);
      let form = new FormData();
      form.append('RoomNumber', this.updateForm.value.roomNumber);
      form.append('Name', this.updateForm.value.name);
      form.append('Description', this.updateForm.value.description);
      form.append('IsAvailable', this.updateForm.value.isAvailable);
      form.append('RoomTypeId', this.updateForm.value.roomTypeId);
      form.append('Price', this.updateForm.value.price);
      form.append('Id', this.room.Id.toString());

      form.append('ValidFrom', this.updateForm.value.validFrom);
        form.append('GoodThru', this.updateForm.value.goodThru);
      
      for (let file of this.newImgObjToAdd) {
        form.append('files', file);
      }

      this.agentService.updateRoom(form).subscribe({
        next: (res: any) => {
          this.sharedService.showLoading(false);
          this.sharedService.showToastMessage('success' + res.Value.message);
        },
        error: (err) => {
          this.sharedService.showLoading(false);
          //this.sharedService.showToastMessage(err.error.Value.message);
          console.log(err);
          
        },
      });
    }
  }

}



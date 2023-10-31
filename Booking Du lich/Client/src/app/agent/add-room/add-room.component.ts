import { Component, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Hotel } from 'src/app/shared/models/hotel/hotel';
import { AgentService } from '../agent.service';
import { AccountService } from 'src/app/account/account.service';
import { SharedService } from 'src/app/shared/shared.service';
import { map, mergeMap } from 'rxjs';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AdminService } from 'src/app/admin/admin.service';
import { ImgShow } from 'src/app/shared/models/image/imgShow';
import { environment } from 'src/environments/environment.development';
import { City } from 'src/app/shared/models/city/city';

@Component({
  selector: 'app-add-room',
  templateUrl: './add-room.component.html',
  styleUrls: ['./add-room.component.scss']
})
export class AddRoomComponent {
  hotelID: number | null = null;
  userId: string = '';

  listNewImgUrls: ImgShow[] = []; // danh sách ảnh mới để hiển thị trên view
  newImgObjToAdd: any = []; // danh sách các đối tượng file gửi lên server
  imgFiles: ImgShow[] = []; // danh sách ảnh cũ
  @ViewChild('fileInput') fileInput!: ElementRef;
  envImgUrl = environment.imgUrl;

  allCities: City[] = [];
  hotelGroup: FormGroup = new FormGroup([]);
  submitted: boolean = false;
  cityId: number = 1;
  imgNames: string[] = [];

  constructor(
    private activatedRoute: ActivatedRoute,
    private adminService: AdminService,
    private formBuilder: FormBuilder,
    private accountService: AccountService,
    private agentService: AgentService,
    private sharedService: SharedService,
    private router: Router
  ) {}

  ngOnInit() {
    this.hotelGroup = this.formBuilder.group({
      id: [''],
      hotelName: ['', [Validators.required]],
      address: ['', [Validators.required]],
      description: ['', [Validators.required]],
    });

    this.activatedRoute.params.subscribe({
      next: (params: any) => {
        if (params['id'] !== undefined) {
          this.hotelID = params['id'];
        }
        this.resetHotelForm();
      },
    });

    this.adminService.getAllCities().subscribe({
      next: (res: any) => {
        this.allCities = res;
      },
      error: (err) => {
        console.log(err);
      },
    });

    this.getUserId();
  }

  private getUserId() {
    this.accountService.user$.subscribe((u) => {
      if (u !== null) {
        this.userId = u.Id;
      }
    });
  }

  private resetHotelForm() {
    if (this.hotelID !== null) {
      this.agentService.getHotelById(this.hotelID).subscribe({
        next: (res: any) => {
          this.hotelGroup.controls['id'].setValue(res.hotel.Id);
          this.hotelGroup.controls['hotelName'].setValue(res.hotel.HotelName);
          this.hotelGroup.controls['address'].setValue(res.hotel.Address);
          this.hotelGroup.controls['description'].setValue(
            res.hotel.Description
          );
          this.cityId = res.hotel.CityId;
          this.imgNames = res.imgNames;

          for (let img of res.imgNames) {
            const arr = img.split('\\');
            const imgName: string = arr[arr.length - 1];

            const imgShow: ImgShow = {
              name: imgName,
              data: img,
            };
            this.imgFiles.push(imgShow);
          }
        },
        error: (err) => {
          console.log(err);
        },
      });
    }
  }

  private clearFile() {
    this.fileInput.nativeElement.value = '';
  }

  onSelectCityChange(event: any) {
    const selectedOption = event.selectedOptions[0];
    this.cityId = selectedOption.value;
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
    if (this.hotelID !== null) {
      this.agentService.deleteAllImgHotel(this.hotelID).subscribe({
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

  removeImgUnSave(data: string) {
    const index = this.listNewImgUrls.findIndex((url) => url.data === data);
    if (index !== -1) {
      this.listNewImgUrls.splice(index, 1);
    }
  }

  removeImgSaved(data: string) {
    this.sharedService.showLoading(true);
    this.agentService.deleteImgHotel(data).subscribe({
      next: (res) => {
        this.sharedService.showToastMessage('successDelete image successfully');
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

  submit() {
    this.submitted = true;
    if (this.hotelID === null) {
      this.addHotel();
    } else {
      this.updateHotel();
    }
  }

  private addHotel() {
    if (this.hotelGroup.valid) {
      this.sharedService.showLoading(true);
      let form = new FormData();
      form.append('HotelName', this.hotelGroup.value.hotelName);
      form.append('Address', this.hotelGroup.value.address);
      form.append('Description', this.hotelGroup.value.description);
      form.append('CityId', this.cityId.toString());
      form.append('PosterID', this.userId);

      for (let file of this.newImgObjToAdd) {
        form.append('files', file);
      }

      this.agentService.addHotel(form).subscribe({
        next: (res: any) => {
          this.sharedService.showLoading(false);
          this.sharedService.showToastMessage('success' + res.Value.message);
          this.router.navigateByUrl('/agent/post-detail/' + res.Value.newHotel.Id );
        },
        error: (err) => {
          this.sharedService.showLoading(false);
          this.sharedService.showToastMessage(err.error.Value.message);
        },
      });
    }
  }

  private updateHotel() {
    if (this.hotelGroup.valid) {
      this.sharedService.showLoading(true);
      let form = new FormData();
      form.append('HotelName', this.hotelGroup.value.hotelName);
      form.append('Address', this.hotelGroup.value.address);
      form.append('Description', this.hotelGroup.value.description);
      form.append('CityId', this.cityId.toString());
      form.append('Id', this.hotelGroup.value.id);

      for (let file of this.newImgObjToAdd) {
        form.append('files', file);
      }

      this.agentService.updateHotel(form).subscribe({
        next: (res: any) => {
          this.sharedService.showLoading(false);
          this.sharedService.showToastMessage('success' + res.Value.message);
        },
        error: (err) => {
          this.sharedService.showLoading(false);
          this.sharedService.showToastMessage(err.error.Value.message);
        },
      });
    }
  }
}

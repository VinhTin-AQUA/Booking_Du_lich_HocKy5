import { Component, ElementRef, ViewChild } from '@angular/core';
import { map, pipe, mergeMap, every } from 'rxjs';

import { AccountService } from 'src/app/account/account.service';
import { AgentService } from '../agent.service';
import { Hotel } from 'src/app/shared/models/hotel/hotel';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AdminService } from 'src/app/admin/admin.service';
import { City } from 'src/app/shared/models/city/city';
import { SharedService } from 'src/app/shared/shared.service';
import { environment } from 'src/environments/environment.development';
import { ImgShow } from 'src/app/shared/models/image/imgShow';

@Component({
  selector: 'app-hotels',
  templateUrl: './hotel.component.html',
  styleUrls: ['./hotel.component.scss'],
})
export class HotelComponent {
  private userId: string | undefined = '';
  hotel: Hotel | null = null;
  imgFiles: ImgShow[] = [];
  envImgUrl = environment.imgUrl;

  formHotelSubmit: FormGroup = new FormGroup([]);
  submitted: boolean = false;
  errorMessage: string[] = [];
  cities: City[] = [];
  cityOfHotel: City = {
    Id: 1,
    CityCode: 1,
    Name: 'string',
    ImgUrl: 'string',
    Accommodations: 1,
  };

  //img
  listNewImgUrls: ImgShow[] = []; // danh sách ảnh mới để hiển thị trên view
  newImgObjToAdd: any = []; // danh sách các đối tượng file gửi lên server

  @ViewChild('fileInput') fileInput!: ElementRef;

  constructor(
    private accountService: AccountService,
    private agentService: AgentService,
    private formBuilder: FormBuilder,
    private adminService: AdminService,
    private sharedService: SharedService
  ) {
    this.formHotelSubmit = this.formBuilder.group({
      Id: [1, [Validators.required]],
      hotelName: ['', [Validators.required]],
      address: ['this.hotel.Address', [Validators.required]],
      description: ['this.hotel.Description', [Validators.required]],
      cityId: ['1'],
    });

    // get all cities
    this.adminService.getAllCities().subscribe({
      next: (res: any) => {
        this.cities = res;
        this.cityOfHotel = this.cities[0];
      },
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
          this.hotel = res.hotel;
          for (let i of res.imgFileNames) {
            const temp = i.split('\\');

            const imgShow: ImgShow = {
              name: temp[temp.length - 1],
              data: i,
            };
            console.log(imgShow);

            this.imgFiles.push(imgShow);
          }

          if (this.hotel !== null && this.hotel.City !== null) {
            this.cityOfHotel = this.hotel.City;
          }
          this.formInit();
        },
        error: (err) => {
          console.log(err);
        },
      });
  }

  private formInit() {
    if (this.hotel !== null) {
      this.formHotelSubmit.controls['Id'].setValue(this.hotel.Id);
      this.formHotelSubmit.controls['hotelName'].setValue(this.hotel.HotelName);
      this.formHotelSubmit.controls['address'].setValue(this.hotel.Address);
      this.formHotelSubmit.controls['description'].setValue(
        this.hotel.Description
      );
    }
  }

  private getImagesHotel() {
    this.agentService.getHotelOfAgent(this.userId).subscribe({
      next: (res: any) => {
        for (let i of res.imgFileNames) {
          const temp = i.split('\\');

          const imgShow: ImgShow = {
            name: temp[temp.length - 1],
            data: i,
          };
          console.log(imgShow);

          this.imgFiles.push(imgShow);
        }
        this.listNewImgUrls = [];
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  private clearFile() {
    this.fileInput.nativeElement.value = '';
  }

   submitHotel() {
    this.submitted = true;
    this.errorMessage = [];

    if (this.formHotelSubmit.valid) {
      let formData = new FormData();
      formData.append('hotelName', this.formHotelSubmit.value.hotelName);
      formData.append('address', this.formHotelSubmit.value.address);
      formData.append('description', this.formHotelSubmit.value.description);
      formData.append('cityId', this.formHotelSubmit.value.cityId);
      formData.append('Id', this.formHotelSubmit.value.Id);

      //this.listImgAdd.forEarch((_file: any) => formData.append('files',_file))
      for (let file of this.newImgObjToAdd) {
        formData.append('files', file);
      }
      this.sharedService.showLoading(true);
      this.agentService.updateHotel(formData).subscribe({
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

  // img
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

  removeImgUnSave(url: string) {
    const index = this.listNewImgUrls.findIndex((u: any) => u.data === url);
    if (index !== -1) {
      this.listNewImgUrls.splice(index, 1);
    }
  }

  removeImgSaved(url: string) {
    console.log(url);

    this.agentService.deleteImgHotel(url).subscribe({
      next: (_) => {
        const index = this.imgFiles.findIndex((u: any) => u === url);
        if (index !== -1) {
          this.imgFiles.splice(index, 1);
        }
        this.sharedService.showToastMessage(
          'success Delete image successfully'
        );
      },
      error: (_) => {
        this.sharedService.showToastMessage('Please try again');
      },
    });
  }

  deleteAllIg() {
    this.agentService.deleteAllImgHotel(this.hotel?.Id).subscribe({
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

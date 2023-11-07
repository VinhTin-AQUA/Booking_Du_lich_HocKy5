import { Component, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AgentTourService } from '../agent-tour.service';
import { map, mergeMap } from 'rxjs'
import { Tour } from 'src/app/shared/models/tour/tour';
import { TourType } from 'src/app/shared/models/tour/tourType';
import { ImgShow } from 'src/app/shared/models/image/imgShow';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { City } from 'src/app/shared/models/city/city';
import { AdminService } from 'src/app/admin/admin.service';
import { AccountService } from 'src/app/account/account.service';
import { SharedService } from 'src/app/shared/shared.service';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-update-tour',
  templateUrl: './update-tour.component.html',
  styleUrls: ['./update-tour.component.scss']
})
export class UpdateTourComponent {
  tour: Tour | null = null;
  allTourTypes: TourType[] = [];
  allCities: City[] = [];
  userId: string | null = null;
  imgBase = environment.imgUrl

  //img
  listNewImgUrls: ImgShow[] = []; // danh sách ảnh mới để hiển thị trên view
  newImgObjToAdd: any = []; // danh sách các đối tượng file gửi lên server
  imgFiles: ImgShow[] = []; // danh sách ảnh cũ
  @ViewChild('fileInput') fileInput!: ElementRef;

  updateGroup: FormGroup = new FormGroup([]);
  submitted: boolean = false;

  constructor(private activatedRoute: ActivatedRoute, 
    private agentTourService: AgentTourService,
    private formBuilder: FormBuilder,
    private adminService: AdminService,
    private accountService: AccountService,
    private sharedService: SharedService) {
  }

  ngOnInit(){
    this.getAllTourTypes();
    this.getAllCities();
    this.getUserId();

    this.activatedRoute.params
    .pipe(
      map((tourId) => {
        return tourId['id'];
      }),
      mergeMap((tourId) => this.agentTourService.getTourById(tourId))
    )
    .subscribe((tour: any) => {
      if (tour !== null) {
        this.tour = tour.tour;
        this.initUpdateGroup();
        for(let fileName of tour.fileNames) {
          const arr = fileName.split('\\');
            const imgName: string = arr[arr.length - 1];

            const imgShow: ImgShow = {
              name: imgName,
              data: fileName,
            };
            this.imgFiles.push(imgShow);
        }
      }
    });

    this.updateGroup = this.formBuilder.group({
      tourName: ['', [Validators.required]],
      tourTypeId: [1],
      cityId: [1],
      tourAddress: ['', [Validators.required]],
      overview: ['', [Validators.required]],
      schedule: ['', [Validators.required]],
      departureLocation: ['', [Validators.required]],
      dropOffLocation: ['', [Validators.required]],
    });
  }

  private initUpdateGroup() {
    if(this.tour !== null) {
      this.updateGroup.controls['tourName'].setValue(this.tour.TourName);
      this.updateGroup.controls['tourTypeId'].setValue(this.tour.TourType.TourTypeId);
      this.updateGroup.controls['cityId'].setValue(this.tour.City.Id);
      this.updateGroup.controls['tourAddress'].setValue(this.tour.TourAddress);
      this.updateGroup.controls['overview'].setValue(this.tour.Overview);
      this.updateGroup.controls['schedule'].setValue(this.tour.Schedule);
      this.updateGroup.controls['departureLocation'].setValue(this.tour.DepartureLocation);
      this.updateGroup.controls['dropOffLocation'].setValue(this.tour.DropOffLocation);
    }
  }

  private getUserId() {
    this.accountService.user$.subscribe((u) => {
      if (u !== null) {
        this.userId = u.Id;
      }
    });
  }

  private getAllTourTypes() {
    this.adminService.getAllTours().subscribe({
      next: (res: any) => {
        this.allTourTypes = res;
      },
    });
  }

  private getAllCities() {
    this.adminService.getAllCities().subscribe({
      next: (res: any) => {
        this.allCities = res;
      },
    });
  }

  private clearFile() {
    this.fileInput.nativeElement.value = '';
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

  removeImgSaved(data: string) {
    this.sharedService.showLoading(true);

    if(this.tour !== null) {
      this.agentTourService.deleteImg(data,this.tour.TourId).subscribe({
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
  }

  submit() {
    this.submitted = true;
    if (this.updateGroup.valid && this.tour !== null) {
      let form = new FormData();

      form.append('TourId', this.tour.TourId.toString());
      form.append('TourName', this.updateGroup.value.tourName);
      form.append('TourTypeId', this.updateGroup.value.tourTypeId);
      form.append('CityId', this.updateGroup.value.cityId);
      form.append('TourAddress', this.updateGroup.value.tourAddress);
      form.append('Overview', this.updateGroup.value.overview);
      form.append('Schedule', this.updateGroup.value.schedule);
      form.append(
        'DepartureLocation',
        this.updateGroup.value.departureLocation
      );
      form.append('DropOffLocation', this.updateGroup.value.dropOffLocation);
      form.append('PosterID', this.updateGroup.value.dropOffLocation);

      for (let file of this.newImgObjToAdd) {
        form.append('files', file);
      }

      this.agentTourService.updateTour(form).subscribe({
        next: (res) => {
          this.sharedService.showToastMessage('successCập nhật tour thành công');
        },
        error: (err) => {
          console.log(err);
          this.sharedService.showToastMessage('Có lỗi xin thử lại');
        },
      });
    }
  }
}

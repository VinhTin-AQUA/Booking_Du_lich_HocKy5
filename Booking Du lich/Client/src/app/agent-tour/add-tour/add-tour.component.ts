import { Component, ElementRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountService } from 'src/app/account/account.service';
import { AdminService } from 'src/app/admin/admin.service';
import { City } from 'src/app/shared/models/city/city';
import { ImgShow } from 'src/app/shared/models/image/imgShow';
import { TourType } from 'src/app/shared/models/tour/tourType';
import { AgentTourService } from '../agent-tour.service';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-add-tour',
  templateUrl: './add-tour.component.html',
  styleUrls: ['./add-tour.component.scss'],
})
export class AddTourComponent {
  userId: string | null = null;

  allTourTypes: TourType[] = [];
  allCities: City[] = [];
  posterID: string = '';

  addTourGroup: FormGroup = new FormGroup([]);
  submitted: boolean = false;

  //img
  listNewImgUrls: ImgShow[] = []; // danh sách ảnh mới để hiển thị trên view
  newImgObjToAdd: any = []; // danh sách các đối tượng file gửi lên server
  imgFiles: ImgShow[] = []; // danh sách ảnh cũ
  @ViewChild('fileInput') fileInput!: ElementRef;

  constructor(
    private adminService: AdminService,
    private formBuilder: FormBuilder,
    private accountService: AccountService,
    private agentTourService: AgentTourService,
    private sharedService: SharedService
  ) {}

  ngOnInit() {
    this.getAllTourTypes();
    this.getAllCities();
    this.getUserId();

    this.addTourGroup = this.formBuilder.group({
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

  private getUserId() {
    this.accountService.user$.subscribe((u) => {
      if (u !== null) {
        this.userId = u.Id;
      }
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

  submit() {
    this.submitted = true;
    if (this.addTourGroup.valid && this.userId !== null) {
      let form = new FormData();

      form.append('TourName', this.addTourGroup.value.tourName);
      form.append('TourTypeId', this.addTourGroup.value.tourTypeId);
      form.append('CityId', this.addTourGroup.value.cityId);
      form.append('TourAddress', this.addTourGroup.value.tourAddress);
      form.append('Overview', this.addTourGroup.value.overview);
      form.append('Schedule', this.addTourGroup.value.schedule);
      form.append(
        'DepartureLocation',
        this.addTourGroup.value.departureLocation
      );
      form.append('DropOffLocation', this.addTourGroup.value.dropOffLocation);
      form.append('PosterID', this.userId.toString());
      form.append('PosterID', this.addTourGroup.value.dropOffLocation);

      for (let file of this.newImgObjToAdd) {
        form.append('files', file);
      }

      this.agentTourService.addTour(form).subscribe({
        next: (res) => {
          this.sharedService.showToastMessage('successThêm tour thành công');
        },
        error: (err) => {
          //console.log(err);
          this.sharedService.showToastMessage('Có lỗi xin thử lại');
        },
      });
    }
  }
}

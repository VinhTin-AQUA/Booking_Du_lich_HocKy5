import { Component, ElementRef, Input, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AccountService } from 'src/app/account/account.service';
import { AdminService } from 'src/app/admin/admin.service';
import { City } from 'src/app/shared/models/city/city';
import { ImgShow } from 'src/app/shared/models/image/imgShow';
import { AgentService } from '../agent.service';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-post-detail',
  templateUrl: './post-detail.component.html',
  styleUrls: ['./post-detail.component.scss'],
})
export class PostDetailComponent {
  hotelID: number | null = null;
  userId: string = '';

  listNewImgUrls: ImgShow[] = []; // danh sách ảnh mới để hiển thị trên view
  newImgObjToAdd: any = []; // danh sách các đối tượng file gửi lên server
  imgFiles: ImgShow[] = []; // danh sách ảnh cũ
  @ViewChild('fileInput') fileInput!: ElementRef;

  allCities: City[] = [];
  hotelGroup: FormGroup = new FormGroup([]);
  submitted: boolean = false;

  constructor(
    private activatedRoute: ActivatedRoute,
    private adminService: AdminService,
    private formBuilder: FormBuilder,
    private accountService: AccountService,
    private agentService: AgentService,
    private sharedService: SharedService
  ) {}

  ngOnInit() {
    this.hotelGroup = this.formBuilder.group({
      hotelName: ['', [Validators.required]],
      address: ['', [Validators.required]],
      description: ['', [Validators.required]],
      cityKey: 2,
    });

    this.activatedRoute.params.subscribe({
      next: (params: any) => {
        if (params['id'] !== null) {
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
    }
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

  deleteAllImg() {
    this.imgFiles = [];
    this.listNewImgUrls = [];
    this.newImgObjToAdd = [];

    //this.agentService.deleteAllImgHotel(this.hotel?.Id).subscribe({
    //  next: (_) => {
    //    this.sharedService.showToastMessage(
    //      'success Delete all images successfully'
    //    );
    //    this.imgFiles = [];
    //    this.listNewImgUrls = [];
    //    this.newImgObjToAdd = [];
    //  },
    //  error: (_) => {
    //    this.sharedService.showToastMessage('Please try again');
    //  },
    //});
  }

  removeImgUnSave(url: string) {}

  removeImgSaved(url: string) {}

  submit() {
    this.submitted = true;

    console.log(this.hotelGroup.value.cityKey);

    //if (this.hotelGroup.valid) {
    //  this.sharedService.showLoading(true);
    //  let form = new FormData();
    //  form.append('HotelName',this.hotelGroup.value.hotelName);
    //  form.append('Address',this.hotelGroup.value.address);
    //  form.append('Description',this.hotelGroup.value.description);
    //  form.append('CityId',this.hotelGroup.value.cityKey.CityId);
    //  form.append('CityCode',this.hotelGroup.value.cityKey.CityCode);
    //  form.append('PosterID',this.userId);

    //  for (let file of this.newImgObjToAdd) {
    //    form.append('files',file);
    //  }

    //  this.agentService.addHotel(form).subscribe({
    //    next: (res:any) => {
    //      console.log(res);
    //      this.sharedService.showLoading(false);
    //      this.sharedService.showToastMessage('success' + res.Value.message);
    //    },
    //    error: (err) => {
    //      this.sharedService.showLoading(false);
    //      this.sharedService.showToastMessage(err.error.Value.message);
    //    }
    //  })
    //}
  }
}

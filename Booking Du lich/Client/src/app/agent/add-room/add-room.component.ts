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

  listNewImgUrls: ImgShow[] = []; // danh sách ảnh mới để hiển thị trên view
  newImgObjToAdd: any = []; // danh sách các đối tượng file gửi lên server
  imgFiles: ImgShow[] = []; // danh sách ảnh cũ
  @ViewChild('fileInput') fileInput!: ElementRef;
  envImgUrl = environment.imgUrl;


  roomGroup: FormGroup = new FormGroup([]);
  submitted: boolean = false;
  imgNames: string[] = [];
  roomType: any;

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
    this.roomGroup = this.formBuilder.group({
      roomNumber: ['',[Validators.required]],
      roomName: ['',[Validators.required]],
      price: ['',[Validators.required, Validators.pattern('^[$]?[0-9]*(\.)?[0-9]?[0-9]?$')]],
      validFrom: [new Date().toISOString().substring(0, 10)],
      goodThru: [new Date().toISOString().substring(0, 10)],
      isAvailable: [true],
      description: ['',[Validators.required]]
    })

    this.activatedRoute.params.subscribe({
      next: (params: any) => {
        if (params['id'] !== undefined) {
          this.hotelID = params['id'];
        }
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

  deleteAllImg() {
              this.imgFiles = [];
          this.listNewImgUrls = [];
          this.newImgObjToAdd = [];
    //if (this.hotelID !== null) {
    //  this.agentService.deleteAllImgHotel(this.hotelID).subscribe({
    //    next: (_) => {
    //      this.sharedService.showToastMessage(
    //        'success Delete all images successfully'
    //      );
    //      this.imgFiles = [];
    //      this.listNewImgUrls = [];
    //      this.newImgObjToAdd = [];
    //    },
    //    error: (_) => {
    //      this.sharedService.showToastMessage('Please try again');
    //    },
    //  });
    //}
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

    if (this.roomGroup.valid) {
      console.log(this.roomGroup.value);
      
    }
  }
  
}

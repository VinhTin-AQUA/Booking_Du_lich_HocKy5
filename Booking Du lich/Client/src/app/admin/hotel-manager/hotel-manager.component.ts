import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AdminService } from '../admin.service';
import { SharedService } from 'src/app/shared/shared.service';
import { Hotel } from 'src/app/shared/models/hotel/hotel';

@Component({
  selector: 'app-hotel-manager',
  templateUrl: './hotel-manager.component.html',
  styleUrls: ['./hotel-manager.component.scss'],
})
export class HotelManagerComponent implements OnInit {
  formAddHotel: FormGroup = new FormGroup([]);
  submitted: boolean = false;
  addHotelModal: boolean = false;
  errorMessage: string = '';
  allHotels: Hotel[] =[]

  constructor(
    private formBuilder: FormBuilder,
    private adminService: AdminService,
    private sharedService: SharedService
  ) {
    this.formAddHotel = this.formBuilder.group({
      hotelName: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
    this.adminService.getAllHotels().subscribe({
      next: (res:any) => {
        this.allHotels = res;
      }
    })
  }

  showAddHotelModal() {
    this.addHotelModal = !this.addHotelModal;
  }

  addHotelSubmit() {
    this.submitted = true;
    this.errorMessage = '';

    if (this.formAddHotel.valid) {
      this.sharedService.showLoading(true);
      this.adminService.addHotel(this.formAddHotel.value).subscribe({
        next: (res: any) => {
          this.sharedService.showToastMessage('success' + res.Value.message);
          this.sharedService.showLoading(false);
          this.addHotelModal = false;
        },
        error: (err) => {
          this.sharedService.showToastMessage(err.error.Value.message);
          this.sharedService.showLoading(false);
          this.addHotelModal = false;
        },
      });
    }
  }
}

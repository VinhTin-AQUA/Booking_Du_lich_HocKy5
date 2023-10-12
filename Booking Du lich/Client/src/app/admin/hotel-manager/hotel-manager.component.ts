import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AdminService } from '../admin.service';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-hotel-manager',
  templateUrl: './hotel-manager.component.html',
  styleUrls: ['./hotel-manager.component.scss'],
})
export class HotelManagerComponent {
  formAddHotel: FormGroup = new FormGroup([]);
  submitted: boolean = false;
  addHotelModal: boolean = false;
  errorMessage: string = '';

  constructor(
    private formBuilder: FormBuilder,
    private adminService: AdminService,
    private sharedService: SharedService
  ) {
    this.formAddHotel = this.formBuilder.group({
      hotelName: ['', [Validators.required]],
    });
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
          this.sharedService.showToastMessage('success' + res.value.message);
          this.sharedService.showLoading(false);
          this.addHotelModal = false;
        },
        error: (err) => {
          this.sharedService.showToastMessage(err.error.value.message);
          this.sharedService.showLoading(false);
          this.addHotelModal = false;
        },
      });
    }
  }
}

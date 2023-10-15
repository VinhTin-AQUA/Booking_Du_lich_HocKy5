import { Component } from '@angular/core';
import { map, pipe, mergeMap } from 'rxjs';

import { AccountService } from 'src/app/account/account.service';
import { AgentService } from '../agent.service';
import { Hotel } from 'src/app/shared/models/hotel/hotel';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AdminService } from 'src/app/admin/admin.service';
import { City } from 'src/app/shared/models/city/city';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-hotels',
  templateUrl: './hotel.component.html',
  styleUrls: ['./hotel.component.scss'],
})
export class HotelComponent {
  private userId: string | undefined = '';
  hotel: Hotel | null = null;
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
          this.hotel = res;
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

      this.agentService.updateHotel(formData).subscribe({
        next: (res: any) => {
          console.log(res.Value.message);
          this.sharedService.showToastMessage('successs' + res.Value.message);
        },
        error: (err) => {
          this.sharedService.showToastMessage(err.error.Value.message);
        },
      });
    }
  }
}

import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { environment } from 'src/environments/environment.development';
import { AdminService } from '../admin.service';
import { City } from 'src/app/shared/models/city/city';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-city-mananger',
  templateUrl: './city-mananger.component.html',
  styleUrls: ['./city-mananger.component.scss'],
})
export class CityManangerComponent implements OnInit {
  file: any;
  name: string = '';
  formSubmit: FormGroup = new FormGroup([]);
  allCities: City[] = [];
  addcityModal: boolean = false;
  imgUrl: string = '';
  submitted: boolean = false;
  _environment = environment;

  // delete city
  deleteCityModal: boolean = false;
  cityDeleteModel: City | null = null;

  //edit
  formEdit: FormGroup = new FormGroup([]);
  editCityModal: boolean = false;

  //search
  searchString: string = '';

  constructor(
    private formBuilder: FormBuilder,
    private adminService: AdminService,
    private sharedService: SharedService
  ) {
    this.formSubmit = this.formBuilder.group({
      cityCode: [, [Validators.required]],
      name: ['', [Validators.required]],
    });

    this.formEdit = this.formBuilder.group({
      id: [, [Validators.required]],
      cityCode: [, [Validators.required]],
      name: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
    this.adminService.getAllCities().subscribe({
      next: (res: any) => {
        this.allCities = res;
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  showAddcityModal() {
    this.addcityModal = true;
  }

  cancel() {
    this.addcityModal = false;
    this.imgUrl = '';
    this.submitted = false;
  }

  getFile(event: any) {
    this.file = event.target.files[0];
    this.name = this.file.name;

    let reader = new FileReader();
    reader.readAsDataURL(this.file);
    reader.onload = (event: any) => {
      this.imgUrl = event.target.result;
    };
  }

  closeImg(file: any) {
    this.imgUrl = '';
    this.file = undefined;
  }

  submit() {
    this.submitted = true;

    if (this.file === undefined) {
      this.sharedService.showToastMessage('Please choose image');
    } else if (this.formSubmit.valid === true) {
      let formData = new FormData();
      formData.append('imgName', this.name);
      formData.append('file', this.file);
      formData.append('name', this.formSubmit.value.name);
      formData.append('cityCode', this.formSubmit.value.cityCode);

      this.adminService.addCity(formData).subscribe({
        next: (res: any) => {
          this.allCities.push(res);
          this.sharedService.showToastMessage('success Add city successfully');
        },
        error: (err) => {
          this.sharedService.showToastMessage(err.error.value.message);
        },
      });
    }
  }

  // delete city
  showDeleteCity(city: City | null) {
    this.deleteCityModal = !this.deleteCityModal;
    this.cityDeleteModel = city;
  }

  deleteCitySubmit() {
    if (this.cityDeleteModel !== null) {
      this.sharedService.showLoading(true);
      this.adminService.deleteCity(this.cityDeleteModel.Id).subscribe({
        next: (res: any) => {
          this.sharedService.showToastMessage(`success${res.Value.message}`);
          this.sharedService.showLoading(false);
          const index = this.allCities.findIndex(
            (x) => x.Id === this.cityDeleteModel?.Id
          );
          if (index !== -1) {
            this.allCities.splice(index, 1);
          }
          this.deleteCityModal = false;
          this.cityDeleteModel = null;
        },
        error: (err) => {
          this.sharedService.showToastMessage(err.error.Value.message);
          this.sharedService.showLoading(false);
          this.deleteCityModal = false;
          this.cityDeleteModel = null;
        },
      });
    }
  }

  //edit
  showEditcityModal(city: City | null) {
    this.editCityModal = !this.editCityModal;

    if (this.editCityModal === true && city !== null) {
      this.formEdit.setValue({
        id: city.Id,
        cityCode: city.CityCode,
        name: city.Name,
      });
      this.imgUrl = `${environment.imgUrl}/${city.PhotoPath}`;
    } else {
      this.submitted = false;
      this.imgUrl = '';
    }
  }

  editSubmit() {
    this.submitted = true;
    if (this.formEdit.valid === true) {
      let formData = new FormData();
      formData.append('imgName', this.name);
      formData.append('file', this.file);

      formData.append('id', this.formEdit.value.id);
      formData.append('cityCode', this.formEdit.value.cityCode);
      formData.append('name', this.formEdit.value.name);

      this.adminService.updateCity(formData).subscribe({
        next: (res: any) => {
          this.sharedService.showToastMessage(
            'success Update city successfully'
          );

          let cityUpdate = this.allCities.find(
            (c) => c.Id === this.formEdit.value.id
          );
          if (cityUpdate !== null && cityUpdate !== undefined) {
            cityUpdate.CityCode = this.formEdit.value.cityCode;
            cityUpdate.Name = this.formEdit.value.name;
            if (this.file !== undefined) {
              cityUpdate.PhotoPath = `/cities/${this.file.name}`;
            }
          }
        },
        error: (err) => {
          this.sharedService.showToastMessage(err.error.value.message);
        },
      });
    }
  }

  //search
  searchCities() {
    this.sharedService.showLoading(true);
    this.adminService.searchCities(this.searchString).subscribe({
      next: (res: any) => {
        this.sharedService.showLoading(false);
        this.allCities = res;
      },
      error: (err) => {
        console.log(err);
        this.sharedService.showLoading(false);
      },
    });
  }
}

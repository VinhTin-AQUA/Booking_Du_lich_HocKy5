
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AdminService } from '../admin.service';
import { City } from 'src/app/shared/models/destionation/city';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-city-mananger',
  templateUrl: './city-mananger.component.html',
  styleUrls: ['./city-mananger.component.scss']
})
export class CityManangerComponent implements OnInit {
  file: any;
  name: string = '';
  formSubmit: FormGroup = new FormGroup([]);
  allCities: City[] = []
  addcityModal: boolean = false;
  imgUrl: string = '';
  submitted: boolean = false;

  constructor(
    private formBuilder: FormBuilder,
    private adminService: AdminService,
    private sharedService: SharedService
  ) {
    this.formSubmit = this.formBuilder.group({
      cityCode: [, [Validators.required]],
      name: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
    this.adminService.getAllCities().subscribe({
      next: (res:any) => {
        console.log(res);
        this.allCities = res
      },
      error: (err) => {
        console.log(err);
      }
    })
  }



  showAddcityModal() {
    this.addcityModal = true;
  }

  cancel() {
    this.addcityModal = false;
    this.imgUrl = '';
    this.submitted = false
  }

  getFile(event: any) {
      this.file = event.target.files[0];
      this.name = this.file.name;

      let reader = new FileReader();
      reader.readAsDataURL(this.file)
      reader.onload = (event: any) => {
        this.imgUrl = event.target.result;
      }
  }

  closeImg(file:any) {
    this.imgUrl = '';
    this.file = undefined;
  }

  submit() {
    this.submitted = true;

    if (this.file === undefined ) {
      this.sharedService.showToastMessage("Please choose image");
    } else if (this.formSubmit.valid === true) {
      let formData = new FormData();
      formData.append('imgName', this.name);
      formData.append('file', this.file);
      formData.append('name', this.formSubmit.value.name);
      formData.append('cityCode', this.formSubmit.value.cityCode);
      
      this.adminService.addCity(formData).subscribe({
        next: (res:any) => {
          this.allCities.push(res);
          this.sharedService.showToastMessage("success Add city successfully");
        },
        error: (err) => {
          this.sharedService.showToastMessage(err.error.value.message);
        }
      })
    }
  }
}

import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-bussiness-part',
  templateUrl: './add-bussiness-part.component.html',
  styleUrls: ['./add-bussiness-part.component.scss']
})
export class AddBussinessPartComponent {
  submitted: boolean = false;
  addPartnerForm: FormGroup = new FormGroup([]);


  constructor(private formBuilder: FormBuilder) {
    this.addPartnerForm = this.formBuilder.group({
      partnerName: ['', [Validators.required]],
      address: ['', [Validators.required]],
      email: ['', [Validators.email, Validators.required]],
      phoneNumber: ['', [Validators.required ,Validators.pattern(
        `(^1300\d{6}$)|(^1800|1900|1902\d{6}$)|(^0[2|3|7|8]{1}[0-9]{8}$)|(^13\d{4}$)|(^04\d{2,3}\d{6}$)`
      ),]],
    })
  }

  inputValidation(field: string): string {
    if (this.submitted === true) {

      if (this.addPartnerForm.hasError('required', field)) {
        return 'input-field-error';
      }

      if (this.addPartnerForm.hasError('email', field)) {
        return 'input-field-error';
      }

      if (this.addPartnerForm.hasError('pattern', field)) {
        return 'input-field-error';
      }
    }
    return 'input-field';
  }

  addSubmit() {
    this.submitted = true;

    if(this.addPartnerForm.valid)  {
      console.log(this.addPartnerForm.value);
      
    }
  }
}
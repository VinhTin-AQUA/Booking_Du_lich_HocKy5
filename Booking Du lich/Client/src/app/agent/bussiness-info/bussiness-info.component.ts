import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-bussiness-info',
  templateUrl: './bussiness-info.component.html',
  styleUrls: ['./bussiness-info.component.scss']
})
export class BussinessInfoComponent {
  submitted: boolean = false;
  bussinessForm: FormGroup = new FormGroup([]);

  constructor(private formBuilder: FormBuilder) {
    this.bussinessForm = this.formBuilder.group({
      partnerName: ['',[Validators.required]],
      address: ['',[Validators.required]],
      email: ['',[Validators.required, Validators.email]],
      phoneNumber: ['',[Validators.required,Validators.pattern(
        `(^1300\d{6}$)|(^1800|1900|1902\d{6}$)|(^0[2|3|7|8]{1}[0-9]{8}$)|(^13\d{4}$)|(^04\d{2,3}\d{6}$)`
      ),]],
    }); 
  }

  inputValidation(field: string) {
    if (this.submitted === true) {  
      if(this.bussinessForm.hasError('required', field)) {
        return 'input-field-error';
      }

      if(this.bussinessForm.hasError('email', field)) {
        return 'input-field-error';
      }

      if(this.bussinessForm.hasError('pattern', field)) {
        return 'input-field-error';
      }
    }
    return 'input-field';
  }

  submit() {
    this.submitted = true
    if (this.bussinessForm.valid) {
      console.log(this.bussinessForm.value);
      
    }
  }
}

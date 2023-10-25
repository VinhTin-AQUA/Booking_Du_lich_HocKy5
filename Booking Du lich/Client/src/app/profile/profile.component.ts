import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from '../account/account.service';
import { SharedService } from '../shared/shared.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent {
  signUpForm: FormGroup = new FormGroup({});
  submitted: boolean = false;
  errorMessages: string[] = [];
  formSuccess: boolean = true;

  constructor(
    private formBuilder: FormBuilder,
    private accountService: AccountService,
    private router: Router,
    private sharedService: SharedService
  ) {}

  ngOnInit(): void {
    this.signUpForm = this.formBuilder.group({
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      email: ['tinhovinh@gmail.com', [Validators.required, Validators.email]],
      address: ['', [Validators.required]],
      phoneNumber: [
        '',
        [
          Validators.required,
          Validators.pattern(
            `(^1300\d{6}$)|(^1800|1900|1902\d{6}$)|(^0[2|3|7|8]{1}[0-9]{8}$)|(^13\d{4}$)|(^04\d{2,3}\d{6}$)`
          ),
        ],
      ],
    });
  }

  inputValidation(field: string): string {
    if (this.submitted === true) {
      if (
        this.signUpForm.hasError('required', field) ||
        (field === 'email' && this.signUpForm.hasError('email', 'email')) ||
        (field === 'password' &&
          this.signUpForm.hasError('minlength', 'password')) ||
        (field === 'password' &&
          this.signUpForm.hasError('maxlength', 'password')) ||
          (field === 'phoneNumber' &&
            this.signUpForm.hasError('pattern', 'phoneNumber'))
      ) {
        return 'input-field-error';
      }
    }
    return 'input-field';
  }

  submit() {
    this.submitted = true;
    this.errorMessages = [];
    this.formSuccess = true;

    if (this.signUpForm.invalid) {
      this.formSuccess = false;
    } else {
      
    }
  }
}

import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { AccountService } from '../account.service';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  signUpForm: FormGroup = new FormGroup({});
  submitted: boolean = false;
  errorMessages: string[] = [];
  errorConfirmPassword: string = '';
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
      email: ['', [Validators.required, Validators.email]],
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
      password: [
        '',
        [
          Validators.required,
          Validators.minLength(6),
          Validators.maxLength(18),
        ],
      ],
      confirmPassword: ['', [Validators.required]],
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

  change() {
    this.errorConfirmPassword = '';
  }

  submit() {
    this.submitted = true;
    this.errorMessages = [];
    this.formSuccess = true;

    if (this.signUpForm.invalid) {
      this.formSuccess = false;
    } else if (
      this.signUpForm.value.password !== this.signUpForm.value.confirmPassword
    ) {
      this.errorConfirmPassword = 'Err';

    } else {
      this.sharedService.showLoading(true);
      this.accountService.signUp(this.signUpForm.value).subscribe({
        next: (res: any) => {
          this.sharedService.showLoading(false);
          this.router.navigateByUrl('/account/send-email-confirm');
        },
        error: (errors: any) => {
          this.sharedService.showLoading(false);
          window.scrollTo(0, 0);
          this.formSuccess = false;
          if (
            errors.error.Errors !== null ||
            errors.error.Errors !== undefined
          ) {
            for (let err of errors.error.Errors) {
              this.errorMessages.push(err);
            }
          } else {
            this.errorMessages.push(errors.error.Value.message);
          }
        },
      });
    }
  }
}

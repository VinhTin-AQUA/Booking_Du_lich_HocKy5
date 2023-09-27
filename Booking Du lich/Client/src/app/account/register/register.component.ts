import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

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

  constructor(private formBuilder: FormBuilder) {}

  ngOnInit(): void {
    this.signUpForm = this.formBuilder.group({
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
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
          this.signUpForm.hasError('maxlength', 'password'))
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

    if (
      this.signUpForm.value.password !== this.signUpForm.value.confirmPassword
    ) {
      this.errorConfirmPassword = 'Err';
    } else if (this.signUpForm.valid) {
      console.log(this.signUpForm.value);
    }
  }
}
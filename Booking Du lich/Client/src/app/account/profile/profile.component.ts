import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { SharedService } from 'src/app/shared/shared.service';
import { AccountService } from '../account.service';
import { map, mergeMap } from 'rxjs';
import { AdminService } from 'src/app/admin/admin.service';
import { UserView } from 'src/app/shared/models/userManager/userView';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent {
  submitForm: FormGroup = new FormGroup({});
  submitted: boolean = false;
  errorMessages: string[] = [];
  formSuccess: boolean = true;

  constructor(
    private formBuilder: FormBuilder,
    private accountService: AccountService,
    private router: Router,
    private sharedService: SharedService,
    private adminService: AdminService
  ) {}

  ngOnInit(): void {
    this.submitForm = this.formBuilder.group({
      id: [''],
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

    this.getInfoUser();
  }

  private getInfoUser() {
    this.accountService.user$
      .pipe(
        map((u) => {
          return u?.Id;
        }),
        mergeMap((userId) => this.adminService.getUserById(userId))
      )
      .subscribe((res: any) => {
        this.initForm(res.user);
      });
  }

  private initForm(user: UserView) {
    this.submitForm.controls['id'].setValue(user.Id);
    this.submitForm.controls['firstName'].setValue(user.FirstName);
    this.submitForm.controls['lastName'].setValue(user.LastName);
    this.submitForm.controls['email'].setValue(user.Email);
    this.submitForm.controls['address'].setValue(user.Address);
    this.submitForm.controls['phoneNumber'].setValue(user.PhoneNumber);
  }

  inputValidation(field: string): string {
    if (this.submitted === true) {
      if (
        this.submitForm.hasError('required', field) ||
        (field === 'email' && this.submitForm.hasError('email', 'email')) ||
        (field === 'password' &&
          this.submitForm.hasError('minlength', 'password')) ||
        (field === 'password' &&
          this.submitForm.hasError('maxlength', 'password')) ||
        (field === 'phoneNumber' &&
          this.submitForm.hasError('pattern', 'phoneNumber'))
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

    if (this.submitForm.invalid) {
      this.formSuccess = false;
    } else {
      this.accountService.updateAccount(this.submitForm.value).subscribe({
        next: (_) => {
          this.sharedService.showToastMessage('successCập nhật thành công');
        },
        error: (err) => {
          this.sharedService.showToastMessage(
            'Có lỗi xảy ra. Vui lòng thử lại'
          );
          console.log(err);
          
        },
      });
    }
  }
}

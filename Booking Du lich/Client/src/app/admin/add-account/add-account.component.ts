import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from 'src/app/account/account.service';
import { SharedService } from 'src/app/shared/shared.service';
import { AdminService } from '../admin.service';
import { AddAgentHotel } from 'src/app/shared/models/business-partner/addAgentHotel';

@Component({
  selector: 'app-add-account',
  templateUrl: './add-account.component.html',
  styleUrls: ['./add-account.component.scss'],
})
export class AddAccountComponent {
  signUpForm: FormGroup = new FormGroup({});
  submitted: boolean = false;
  formSuccess: boolean = true;
  busPartId: number | null = null;

  constructor(
    private formBuilder: FormBuilder,
    private accountService: AccountService,
    private router: Router,
    private sharedService: SharedService,
    private activatedRoute: ActivatedRoute,
    private adminService: AdminService
  ) {}

  ngOnInit(): void {
    this.signUpForm = this.formBuilder.group({
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      agentType: ['1', [Validators.required]],
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
        'abc123',
        [
          Validators.required,
          Validators.minLength(6),
          Validators.maxLength(18),
        ],
      ],
    });

    this.getParams();
  }

  private getParams() {
    this.activatedRoute.params.subscribe({
      next: (params: any) => {
        if (params['id'] === undefined) {
          this.signUpForm.controls['agentType'].setValue('2');
        } else {
          this.busPartId = params['id'];
        }
      },
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
    this.formSuccess = true;

    if (this.signUpForm.invalid) {
      this.formSuccess = false;
      return;
    }

    if (this.busPartId !== null) {
      this.addAgentHotel();
    } else {
      this.addAgentTour();
    }
  }

  private addAgentHotel() {
    if(this.busPartId !== null) {
      const agent: AddAgentHotel = {
        FirstName: this.signUpForm.value.firstName,
        LastName: this.signUpForm.value.lastName,
        PhoneNumber: this.signUpForm.value.phoneNumber,
        Email: this.signUpForm.value.email,
        Address: this.signUpForm.value.address,
        Password: this.signUpForm.value.password,
        BusPartId: this.busPartId,
      }
  
  
      this.adminService.addAgentHotel(agent).subscribe({
        next: (_) => {
          this.sharedService.showToastMessage(
            'successThêm tài khoản đối tác thành công'
          );
        },
        error: (err) => {
          console.log(err);
          this.sharedService.showToastMessage('Có lỗi vui lòng thử lại');
        },
      });
    }
  }

  private addAgentTour() {

  }
}

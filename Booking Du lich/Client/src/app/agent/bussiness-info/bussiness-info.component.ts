import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { map, mergeMap } from 'rxjs';
import { AccountService } from 'src/app/account/account.service';
import { AgentService } from '../agent.service';
import { BusinessPartner } from 'src/app/shared/models/business-partner/businessPartner';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-bussiness-info',
  templateUrl: './bussiness-info.component.html',
  styleUrls: ['./bussiness-info.component.scss'],
})
export class BussinessInfoComponent {
  submitted: boolean = false;
  bussinessForm: FormGroup = new FormGroup([]);
  userId: string | null = null;
  busPart: BusinessPartner | null = null;

  constructor(
    private formBuilder: FormBuilder,
    private accountService: AccountService,
    private agentService: AgentService,
    private sharedService: SharedService
  ) {}

  ngOnInit() {
    this.bussinessForm = this.formBuilder.group({
      id: [''],
      partnerName: ['', [Validators.required]],
      address: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
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

    this.getBusPart();
  }

  private getBusPart() {
    this.accountService.user$
      .pipe(
        map((u) => {
          if (u !== null) {
            this.userId = u.Id;
            return u.Id;
          }
          return null;
        }),
        mergeMap((id) => this.agentService.getBusinessPartner(id))
      )
      .subscribe({
        next: (res: any) => {
          this.busPart = res;
          this.initForm();
        },
        error: (err) => {
          console.log(err);
        },
      });
  }

  private initForm() {
    if (this.busPart !== null) {
      this.bussinessForm.controls['id'].setValue(this.busPart.Id);
      this.bussinessForm.controls['partnerName'].setValue(
        this.busPart.PartnerName
      );
      this.bussinessForm.controls['address'].setValue(this.busPart.Address);
      this.bussinessForm.controls['email'].setValue(this.busPart.Email);
      this.bussinessForm.controls['phoneNumber'].setValue(
        this.busPart.PhoneNumber
      );
    }
  }

  inputValidation(field: string) {
    if (this.submitted === true) {
      if (this.bussinessForm.hasError('required', field)) {
        return 'input-field-error';
      }

      if (this.bussinessForm.hasError('email', field)) {
        return 'input-field-error';
      }

      if (this.bussinessForm.hasError('pattern', field)) {
        return 'input-field-error';
      }
    }
    return 'input-field';
  }

  submit() {
    this.submitted = true;

    if (this.bussinessForm.valid && this.busPart !== null) {
      this.sharedService.showLoading(true);
      this.agentService.updateBusPartber(this.bussinessForm.value).subscribe({
        next: (_) => {
          this.sharedService.showToastMessage('successCập nhật thành công');
          this.sharedService.showLoading(false);
        },
        error: (err) => {
          console.log(err);
          this.sharedService.showToastMessage('Có lỗi. Vui lòng thử lại');
          this.sharedService.showLoading(false);
        },
      });
    }
  }
}

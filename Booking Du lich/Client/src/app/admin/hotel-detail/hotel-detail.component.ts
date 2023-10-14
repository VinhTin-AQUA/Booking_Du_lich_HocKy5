import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription, share } from 'rxjs';
import { Hotel } from 'src/app/shared/models/hotel/hotel';
import { SharedService } from 'src/app/shared/shared.service';
import { AdminService } from '../admin.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Agent } from 'src/app/shared/models/hotel/addAgent';

@Component({
  selector: 'app-hotel-detail',
  templateUrl: './hotel-detail.component.html',
  styleUrls: ['./hotel-detail.component.scss'],
})
export class HotelDetailComponent implements OnInit {
  hotel: Hotel | null = null;
  agents: any;
  hotelId: string | null = '';
  signUpForm: FormGroup = new FormGroup({});
  submitted: boolean = false;
  isShowedAgent: boolean = false;
  erroMessage: string[] = [];
  deleteAgent: boolean = false;
  agentToDelete: any;

  constructor(
    private sharedService: SharedService,
    private activaredRoute: ActivatedRoute,
    private adminService: AdminService,
    private formBuilder: FormBuilder,
    private router: Router
  ) {
    this.activaredRoute.paramMap.subscribe((param) => {
      this.hotelId = param.get('id');
    });

    this.adminService.getHotelById(this.hotelId).subscribe({
      next: (res: any) => {
        this.hotel = res;
        this.agents = res.Agents;
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  ngOnInit(): void {
    this.signUpForm = this.formBuilder.group({
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', [Validators.required]],
      password: ['abc123', [Validators.required]],
    });
  }

  private resetSignUpForm() {
    this.signUpForm.controls['firstName'].setValue('');
    this.signUpForm.controls['lastName'].setValue('');
    this.signUpForm.controls['email'].setValue('');
    this.signUpForm.controls['phoneNumber'].setValue('');
    this.signUpForm.controls['password'].setValue('abc123');

    this.submitted = false;
      this.erroMessage = [];
  }

  deleteHotel() {
    this.sharedService.showLoading(true);
    this.adminService.deleteHotel(this.hotelId).subscribe({
      next: (res: any) => {
        this.sharedService.showToastMessage('success' + res.Value.message);
        this.sharedService.showLoading(false);
        this.router.navigateByUrl('/admin/hotel-management');
      },
      error: (err) => {
        this.sharedService.showLoading(false);
        this.sharedService.showToastMessage(err.error.Value.message);
      }
    })
  }

  showaddAgent() {
    this.isShowedAgent = !this.isShowedAgent;

    if (this.isShowedAgent === false) {
      this.resetSignUpForm()
    }
  }

  inputValidation(field: string): string {
    if (this.submitted === true) {
      if (
        this.signUpForm.hasError('required', field) ||
        (field === 'email' && this.signUpForm.hasError('email', 'email'))
      ) {
        return 'input-field-error';
      }
    }
    return 'input-field';
  }

  submit() {
    this.submitted = true;
    this.erroMessage = [];
    if (this.signUpForm.valid) {
      const model: Agent = {
        HotelId: this.hotelId,
        FirstName: this.signUpForm.value.firstName,
        LastName: this.signUpForm.value.lastName,
        Email: this.signUpForm.value.email,
        EmailConfirmed: false,
        LockoutEnd: null,
        PhoneNumber: this.signUpForm.value.phoneNumber,
        Password: this.signUpForm.value.password,
      };

      this.sharedService.showLoading(true);

      this.adminService.addAgent(model).subscribe({
        next: (res: any) => {
          this.sharedService.showToastMessage('success' + res.Value.message);
          this.isShowedAgent = false;
          this.agents.push(model);
          this.sharedService.showLoading(false);
          this.resetSignUpForm();
          
        },
        error: (err) => {
          if (err.error.Errors !== undefined) {
            for (let _err of err.error.Errors) {
              this.erroMessage.push(_err);
            }
          } else {
            this.erroMessage.push(err.error.Value.message);
          }
          this.sharedService.showLoading(false);
          this.resetSignUpForm();
        },
      });
    }
  }

  showDeleteAgent(agentToDelete: any) {
    this.deleteAgent = !this.deleteAgent;
    this.agentToDelete = agentToDelete;
  }

  deleteAgentSubmit() {
    this.sharedService.showLoading(true);
    this.adminService.deleteUser(this.agentToDelete.Email).subscribe({
      next: (res) => {
        this.sharedService.showToastMessage('success Delete user successfully');

        const index = this.agents.findIndex(
          (x: any) => x.Email === this.agentToDelete?.Email
        );
        if (index !== -1) {
          this.agents.splice(index, 1);
        }
        this.agentToDelete = null;
        this.deleteAgent = false;
        this.sharedService.showLoading(false);
      },
      error: (err) => {
        this.sharedService.showToastMessage(err.error.Value.message);
        this.sharedService.showLoading(false);
      },
    });
  }

  lockUser(tag: string, agent: any) {
    if (tag === 'lock') {
      this.adminService.lockUser(agent.Email).subscribe({
        next: (res: any) => {
          this.sharedService.showToastMessage('success Lock user successfully');
          agent.LockoutEnd = res.Value.lockoutEnd;
        },
        error: (err) => {
          this.sharedService.showToastMessage(err.error.Value.message);
        },
      });
    } else if (tag === 'un-lock') {
      this.adminService.unlockUser(agent.Email).subscribe({
        next: (res: any) => {
          this.sharedService.showToastMessage(
            'success Unlock user successfully'
          );
          agent.LockoutEnd = null;
        },
        error: (err) => {
          this.sharedService.showToastMessage(err.error.Value.message);
        },
      });
    }
  }
}

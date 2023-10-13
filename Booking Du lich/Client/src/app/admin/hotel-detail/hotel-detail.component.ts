import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
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
  id: string | null = '';
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
    private formBuilder: FormBuilder
  ) {
    this.activaredRoute.paramMap.subscribe((param) => {
      this.id = param.get('id');
    });

    this.adminService.getHotelById(this.id).subscribe({
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

  showaddAgent() {
    this.isShowedAgent = !this.isShowedAgent;
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
        HotelId: this.id,
        FirstName: this.signUpForm.value.firstName,
        LastName: this.signUpForm.value.lastName,
        Email: this.signUpForm.value.email,
        PhoneNumber: this.signUpForm.value.phoneNumber,
        Password: this.signUpForm.value.password,
      };

      this.adminService.addAgent(model).subscribe({
        next: (res: any) => {
          this.sharedService.showToastMessage('success' + res.Value.message);
          this.isShowedAgent = false;
          this.agents.push(model);
        },
        error: (err) => {
          if (err.error.Errors !== undefined) {
            for (let _err of err.error.Errors) {
              this.erroMessage.push(_err);
            }
          } else {
            this.erroMessage.push(err.error.Value.message);
          }
        },
      });
    }
  }

  showDeleteAgent(agentToDelete: any) {
    this.deleteAgent = !this.deleteAgent;
    this.agentToDelete = agentToDelete;
  }

  deleteAgentSubmit() {
    this.adminService.deleteAgent(this.agentToDelete.Id).subscribe({
      next: (res) => {
        console.log(res);
        this.sharedService.showToastMessage(
          'success Delete user successfully'
        );

        const index = this.agents.findIndex(
          (x:any) => x.Id === this.agentToDelete?.Id
        );
        if (index !== -1) {
          this.agents.splice(index, 1);
        }
        this.agentToDelete = null;
      },
      error: (err) => {
        this.sharedService.showToastMessage(err.error.Value.message);
      }
    });
  }

  lockUser(tag: string, agent: any) {
    if (tag === 'lock') {
      this.adminService.lockUser(agent.Id).subscribe({
        next: (res: any) => {
          this.sharedService.showToastMessage('success Lock user successfully');
          agent.LockoutEnd = res.Value.lockoutEnd;
        },
        error: (err) => {
          this.sharedService.showToastMessage(err.error.Value.message);
        },
      });
    } else if (tag === 'un-lock') {
      this.adminService.unlockUser(agent.Id).subscribe({
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

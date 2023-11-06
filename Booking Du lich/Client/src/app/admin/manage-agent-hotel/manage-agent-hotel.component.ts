import { Component } from '@angular/core';
import { AdminService } from '../admin.service';
import { getLocaleFirstDayOfWeek } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { map, mergeMap } from 'rxjs';
import { UserView } from 'src/app/shared/models/userManager/userView';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-manage-agent-hotel',
  templateUrl: './manage-agent-hotel.component.html',
  styleUrls: ['./manage-agent-hotel.component.scss']
})
export class ManageAgentHotelComponent {
  users: UserView[] = [];
  deleteUser: boolean = false;
  userToDelete: UserView | null = null;
  
  constructor(private adminService: AdminService,
    private activatedRoute: ActivatedRoute,
    private sharedService: SharedService ) {

  }

  ngOnInit() {
    this.getAgentHotels();
  }

  private getAgentHotels() {
    this.activatedRoute.params.pipe(
      map((params) => {
        return params['id'];
      }),
      mergeMap((id) => this.adminService.getAgentHotels(id))
    ).subscribe({
      next: (res: any) => {
        this.users = res 
      }
    })
  }

  lockUser(tag: string, user: UserView) {
    if (tag === 'lock') {
      this.adminService.lockUser(user.Email).subscribe({
        next: (res: any) => {
          this.sharedService.showToastMessage('success Lock user successfully');
          user.LockoutEnd = res.Value.lockoutEnd;
        },
        error: (err) => {
          this.sharedService.showToastMessage(err.error.Value.message);
        },
      });
    } else if (tag === 'un-lock') {
      this.adminService.unlockUser(user.Email).subscribe({
        next: (res: any) => {
          this.sharedService.showToastMessage(
            'success Unlock user successfully'
          );
          user.LockoutEnd = null;
        },
        error: (err) => {
          this.sharedService.showToastMessage(err.error.Value.message);
        },
      });
    }
  }

  showDeleteUser(user: UserView | null) {
    this.userToDelete = user;
    this.deleteUser = !this.deleteUser;
  }

  deleteUserSubmit() {
    if (this.userToDelete !== null) {
      this.adminService.deleteUser(this.userToDelete.Email).subscribe({
        next: (res) => {
          this.sharedService.showToastMessage(
            'success Delete user successfully'
          );

          const index = this.users.findIndex(
            (x) => x.Email === this.userToDelete?.Email
          );
          if (index !== -1) {
            this.users.splice(index, 1);
          }
          this.userToDelete = null;
        },
        error: (err) => {
          this.sharedService.showToastMessage(err.error.Value.message);
        },
      });
    }
  }

}

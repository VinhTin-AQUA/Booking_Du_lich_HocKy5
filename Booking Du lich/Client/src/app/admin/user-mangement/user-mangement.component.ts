import { Component } from '@angular/core';
import { AdminService } from '../admin.service';
import { UserView } from 'src/app/shared/models/userManager/userView';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-user-mangement',
  templateUrl: './user-mangement.component.html',
  styleUrls: ['./user-mangement.component.scss'],
})
export class UserMangementComponent {
  currentPage: number = 0;
  pageSize: number = 10;
  totalPages: number = 0;

  users: UserView[] = [];
  deleteUser: boolean = false;
  userToDelete: UserView | null = null;

  constructor(
    private adminService: AdminService,
    private shareService: SharedService
  ) {
    this.getUsers();
  }

  private getUsers() {
    this.adminService.getUsers(this.currentPage, this.pageSize).subscribe({
      next: (res: any) => {
        this.users = res.users;
        this.totalPages = Math.floor(res.totalUser / this.pageSize);

        if (res.totalUser % this.pageSize !== 0) {
          this.totalPages += 1;
        }
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  prevPage() {
    this.currentPage -= 1;
    if (this.currentPage < 0) {
      this.currentPage = 0;
    }
    this.getUsers();
  }

  nextPage() {
    this.currentPage += 1;
    if (this.currentPage >= this.totalPages) {
      this.currentPage = this.totalPages - 1;
    }
    this.getUsers();
  }

  lockUser(tag: string, user: UserView) {
    if (tag === 'lock') {
      this.adminService.lockUser(user.id).subscribe({
        next: (res: any) => {
          this.shareService.showToastMessage('success Lock user successfully');
          user.lockoutEnd = res.value.lockoutEnd;
        },
        error: (err) => {
          this.shareService.showToastMessage(err.error.value.message);
        },
      });
    } else if (tag === 'un-lock') {
      this.adminService.unlockUser(user.id).subscribe({
        next: (res: any) => {
          this.shareService.showToastMessage(
            'success Unlock user successfully'
          );
          user.lockoutEnd = null;
        },
        error: (err) => {
          this.shareService.showToastMessage(err.error.value.message);
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
      this.adminService.deleteUser(this.userToDelete.id).subscribe({
        next: (res) => {
          this.shareService.showToastMessage(
            'success Delete user successfully'
          );

          const index = this.users.findIndex(
            (x) => x.id === this.userToDelete?.id
          );
          if (index !== -1) {
            this.users.splice(index, 1);
          }
          this.userToDelete = null;
        },
        error: (err) => {
          this.shareService.showToastMessage(err.error.value.message);
        },
      });
    }
  }
}

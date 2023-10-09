import { Component } from '@angular/core';
import { AdminService } from '../admin.service';
import { UserView } from 'src/app/shared/models/userManager/userView';

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

  constructor(private adminService: AdminService) {
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
      this.currentPage = 0
    }
    this.getUsers();
  }
  nextPage() {
    this.currentPage += 1;
    if(this.currentPage >= this.totalPages) {
      this.currentPage = this.totalPages - 1;
    }
    this.getUsers();
  }
}

import { Component } from '@angular/core';
import { AdminService } from '../admin.service';
import { UserView } from 'src/app/shared/models/userManager/userView';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-manage-agent-tour',
  templateUrl: './manage-agent-tour.component.html',
  styleUrls: ['./manage-agent-tour.component.scss'],
})
export class ManageAgentTourComponent {
  agents: UserView[] = [];

  constructor(
    private adminService: AdminService,
    private sharedServide: SharedService
  ) {}

  ngOnInit() {
    this.getAgentTours();
  }

  private getAgentTours() {
    this.adminService.getAgentTours().subscribe({
      next: (res: any) => {
        this.agents = res;
      },
    });
  }

  delete(email: string) {
    this.sharedServide.showLoading(true);
    this.adminService.deleteUser(email).subscribe({
      next: (_) => {
        this.sharedServide.showToastMessage('successXóa thành công.');
        this.sharedServide.showLoading(false);

        const index = this.agents.findIndex(a => a.Email === email);
        if(index !== -1) {
          this.agents.splice(index, 1)
        }

      },
      error: (err) => {
        this.sharedServide.showToastMessage('Xảy ra lỗi. Vui lòng thử lại.');
        this.sharedServide.showLoading(false);
      },
    });
  }
}

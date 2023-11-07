import { Component } from '@angular/core';
import { BusinessPartner } from 'src/app/shared/models/business-partner/businessPartner';
import { AdminService } from '../admin.service';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-bussiness-partner',
  templateUrl: './bussiness-partner.component.html',
  styleUrls: ['./bussiness-partner.component.scss'],
})
export class BussinessPartnerComponent {
  businessPartners: BusinessPartner[] = [];

  constructor(
    private adminService: AdminService,
    private sharedService: SharedService
  ) {}

  ngOnInit() {
    this.adminService.getAllBusinessPartners().subscribe({
      next: (res: any) => {
        this.businessPartners = res;
      },
    });
  }

  deleteBusPart(id: number) {
    this.adminService.deleteBusPart(id).subscribe({
      next: (_) => {
        this.sharedService.showToastMessage('successXóa thành công');

        const index = this.businessPartners.findIndex((bp) => bp.Id === id);
        if (index !== -1) {
          this.businessPartners.splice(index, 1);
        }
      },
      error: (er) => {
        console.log(er);
        this.sharedService.showToastMessage('Có lỗi. Vui lòng thử lại');
      },
    });
  }
}

import { Component } from '@angular/core';
import { EmployeeService } from '../employee.service';
import { Hotel } from 'src/app/shared/models/hotel/hotel';
import { SharedService } from 'src/app/shared/shared.service';
import { AccountService } from 'src/app/account/account.service';

@Component({
  selector: 'app-post-censorship',
  templateUrl: './post-censorship.component.html',
  styleUrls: ['./post-censorship.component.scss'],
})
export class PostCensorshipComponent {
  activated: string = '1';
  hotels: Hotel[] = [];
  userId: string | null = null;

  constructor(
    private employeeService: EmployeeService,
    private sharedService: SharedService,
    private accountService: AccountService
  ) {}

  ngOnInit() {
    this.getHotelNotApproved();
    this.getUserId();
  }

  private getHotelNotApproved() {
    this.sharedService.showLoading(true);
    this.employeeService.hotelNotApproved().subscribe({
      next: (res: any) => {
        this.hotels = [];
        this.hotels = res;
        this.sharedService.showLoading(false);
      },
    });
  }

  private getHotelApproved() {
    this.sharedService.showLoading(true);

    this.employeeService.hotelApproved().subscribe({
      next: (res: any) => {
        this.hotels = [];
        this.hotels = res;
        this.sharedService.showLoading(false);
        console.log(res);
        
      },
    });
  }

  private getUserId() {
    this.accountService.user$.subscribe((u) => {
      if (u !== null) {
        this.userId = u.Id;
      }
    });
  }

  censored() {
    this.activated = '0';
    this.getHotelApproved();
    
  }

  dontCensor() {
    this.activated = '1';
    this.getHotelNotApproved();
  }

  approve(hotelId: number) {
    this.sharedService.showLoading(true);
    if (this.userId !== null) {
      this.employeeService.approve(this.userId, hotelId).subscribe({
        next: () => {
          const index = this.hotels.findIndex((h) => h.Id === hotelId);
          if (index !== -1) {
            this.hotels.splice(index, 1);
          }
          this.sharedService.showToastMessage('successĐã duyệt');
          this.sharedService.showLoading(false);
        },
        error: (err) => {
          console.log(err);
          this.sharedService.showToastMessage(
            'Có lỗi xảy ra. Vui lòng thử lại.'
          );
          this.sharedService.showLoading(false);
        },
      });
    }
  }
}

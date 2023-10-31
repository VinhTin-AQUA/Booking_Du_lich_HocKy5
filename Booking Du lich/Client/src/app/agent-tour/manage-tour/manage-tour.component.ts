import { Component } from '@angular/core';
import { Tour } from 'src/app/shared/models/tour/tour';
import { AgentTourService } from '../agent-tour.service';
import { AccountService } from 'src/app/account/account.service';
import { map, mergeMap } from 'rxjs';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-manage-tour',
  templateUrl: './manage-tour.component.html',
  styleUrls: ['./manage-tour.component.scss'],
})
export class ManageTourComponent {
  tours: Tour[] = [];
  isShowDeleted: boolean = false;
  tourIdDelete: number | null = null;
  constructor(
    private agentTourService: AgentTourService,
    private accountService: AccountService,
    private sharedService: SharedService
  ) {}

  ngOnInit() {
    this.getAllTour();
  }

  private getAllTour() {
    this.sharedService.showLoading(true);
    this.accountService.user$
      .pipe(
        map((u) => {
          return u;
        }),
        mergeMap((u) => this.agentTourService.getToursOfPoster(u?.Id))
      )
      .subscribe({
        next: (res: any) => {
          this.tours = res;
          this.sharedService.showLoading(false);
        },
      });
  }

  private resetListTout() {
    const index = this.tours.findIndex((t) => t.TourId === this.tourIdDelete);
    if (index !== -1) {
      this.tours.splice(index, 1);
    }
  }

  showDelete(tourId: number | null) {
    this.isShowDeleted = !this.isShowDeleted;

    if (this.isShowDeleted === true) {
      this.tourIdDelete = tourId;
    } else {
      this.resetListTout();
    }
  }

  deleteTour() {
    if (this.tourIdDelete !== null) {
      this.agentTourService.deleteTour(this.tourIdDelete).subscribe({
        next: (_) => {
          this.sharedService.showToastMessage('successXóa tour thành công');
          this.showDelete(null);
        },
        error: (err) => {
          this.sharedService.showToastMessage('Có lỗi khi xóa. Hãy thử lại');
        },
      });
    }
  }
}

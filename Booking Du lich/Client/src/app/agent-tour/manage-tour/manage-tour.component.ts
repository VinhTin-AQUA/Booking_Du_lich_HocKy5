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

  deleteTour() {
    
  }
}

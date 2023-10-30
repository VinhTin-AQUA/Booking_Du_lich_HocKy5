import { state } from '@angular/animations';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Hotel } from 'src/app/shared/models/hotel/hotel';
import { AgentService } from '../agent.service';
import { AccountService } from 'src/app/account/account.service';
import { map, mergeMap } from 'rxjs';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.scss'],
})
export class PostComponent {
  hotels: Hotel[] = [];
  userId: string | null = null;

  constructor(
    private router: Router,
    private agentService: AgentService,
    private accountService: AccountService,
    private sharedService: SharedService
  ) {}

  ngOnInit() {
    this.getHotelsOfAgent();
  }

  private getHotelsOfAgent() {
    this.accountService.user$
      .pipe(
        map((u) => {
          if (u !== null) {
            this.userId = u?.Id;
          }
          return u;
        }),
        mergeMap((u) => this.agentService.getHotelsOfAgentHotel(u?.Id))
      )
      .subscribe({
        next: (res: any) => {
          this.hotels = res.hotels;
        },
        error: (err) => {
          console.log(err);
        },
      });
  }

  deleteHotel(hotelId: number) {
    this.sharedService.showLoading(true);
    this.agentService.deleteHotel(hotelId).subscribe({
      next: (_) => {
        this.sharedService.showLoading(false);
        this.sharedService.showToastMessage('successXóa bài viết thành công')
        const index = this.hotels.findIndex(h => h.Id === hotelId);
        if(index !== -1) {
          this.hotels.splice(index,1);
        }
      },
      error: (err) => {
        this.sharedService.showLoading(false);
        this.sharedService.showToastMessage('Có lỗi xảy ra. Vui lòng thử lại')
      }
    })
  }
}

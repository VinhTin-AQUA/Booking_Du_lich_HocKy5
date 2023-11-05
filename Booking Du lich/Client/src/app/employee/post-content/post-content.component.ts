import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { map, mergeMap } from 'rxjs';
import { AccountService } from 'src/app/account/account.service';

import { AgentService } from 'src/app/agent/agent.service';
import { Hotel } from 'src/app/shared/models/hotel/hotel';
import { ImgShow } from 'src/app/shared/models/image/imgShow';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-post-content',
  templateUrl: './post-content.component.html',
  styleUrls: ['./post-content.component.scss'],
})
export class PostContentComponent {
  hotelId: number | null = null;
  hotel: Hotel | null = null;
  envImgUrl = environment.imgUrl;
  imgFiles: ImgShow[] = []

  constructor(
    private activatedRoute: ActivatedRoute,
    private agentService: AgentService,
  ) {}

  ngOnInit() {
    this.activatedRoute.params
      .pipe(
        map((params: any) => {
          return params;
        }),
        mergeMap((params) => this.agentService.getHotelById(params.id))
      )
      .subscribe({
        next: (res: any) => {
          this.hotel = res.hotel;

          for (let img of res.imgNames) {
            const arr = img.split('\\');
            const imgName: string = arr[arr.length - 1];

            const imgShow: ImgShow = {
              name: imgName,
              data: img,
            };
            this.imgFiles.push(imgShow);
          }
        },
      });
  }
}

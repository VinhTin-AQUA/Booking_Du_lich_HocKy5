import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { map, mergeMap } from 'rxjs'


import { Hotel } from 'src/app/shared/models/hotel/hotel';
import { ImgShow } from 'src/app/shared/models/image/imgShow';
import { environment } from 'src/environments/environment.development';
import { AgentService } from '../agent.service';
import { Room } from 'src/app/shared/models/room/room';

@Component({
  selector: 'app-edit-room',
  templateUrl: './edit-room.component.html',
  styleUrls: ['./edit-room.component.scss']
})
export class EditRoomComponent {
  room: Room | null = null;
  envImgUrl = environment.imgUrl
  imgFiles: ImgShow[] =[]

  constructor(private activatedRoute: ActivatedRoute,
    private agentService: AgentService) {

  }

  ngOnInit() {
    this.activatedRoute.params
      .pipe(
        map((params: any) => {
          return params;
        }),
        mergeMap((params) => this.agentService.getRoomById(params['id']))
      )
      .subscribe({
        next: (res: any) => {
          this.room = res.room;

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

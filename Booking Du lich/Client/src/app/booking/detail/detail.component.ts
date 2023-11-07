import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AgentService } from 'src/app/agent/agent.service';
import { Hotel } from 'src/app/shared/models/hotel/hotel';
import { ImgShow } from 'src/app/shared/models/image/imgShow';
import { Room } from 'src/app/shared/models/room/room';
import { environment } from 'src/environments/environment.development';
import { BookingService } from '../booking.service';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss']
})
export class DetailComponent {
  cityName: string = '';
  cityId: number = -1;
  hotelId: number = -1;
  hotel: Hotel | null = null;
  hotelImg: ImgShow[] = []
  imgBase = environment.imgUrl;
  rooms: Room[] = [];
  fileNames: string[] = []

  baseImg = environment.imgUrl;

  //======================================
  slider: any;
  defaultTransform: any;

  constructor(private activatedRoute: ActivatedRoute,
    private agentService: AgentService,
    private bookingService: BookingService) {

  }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((params) => {
      this.cityName = params['cityName'];
      this.cityId = params['id'];
      this.hotelId = params['hotelId'];
    });

    this.getHotel();
    this.getRoomsOfHotel();

    this.slider = document.getElementById("slider");
    this.defaultTransform = 0
  }

  goNext() {
    this.defaultTransform = this.defaultTransform - 398;
    if (Math.abs(this.defaultTransform) >= this.slider.scrollWidth / 1.7) this.defaultTransform = 0;
    this.slider.style.transform = "translateX(" + this.defaultTransform + "px)";
  }

  goPrev() {

    if (Math.abs(this.defaultTransform) === 0) this.defaultTransform = 0;
    else this.defaultTransform = this.defaultTransform + 398;
    this.slider.style.transform = "translateX(" + this.defaultTransform + "px)";
  }

  private getHotel() {
    this.agentService.getHotelById(this.hotelId).subscribe((h: any) => {
      this.hotel = h.hotel;

      for (let img of h.imgNames) {
        const arr = img.split('\\');
        const imgName: string = arr[arr.length - 1];

        const imgShow: ImgShow = {
          name: imgName,
          data: img,
        };
        this.hotelImg.push(imgShow);
      }

      console.log(this.hotelImg);
    })
  }


  private getRoomsOfHotel() {
    this.bookingService.getRoomsOfHotel(this.hotelId).subscribe({
      next: (res: any) => {
        this.rooms = res.Value.Rooms
        this.fileNames = res.Value.fileNames;
        console.log(this.fileNames);
        
      },
      error: (err) => {
        console.log(err);

      }
    })
  }



}

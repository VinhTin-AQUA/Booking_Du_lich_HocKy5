import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-city',
  templateUrl: './city.component.html',
  styleUrls: ['./city.component.scss']
})
export class CityComponent {
  cityName: string | null = '';
  private subcription: Subscription | null = null;
  
  constructor(
    private activatedRoute: ActivatedRoute, 
    private sharedService: SharedService,
    private router: Router) {
    
  }

  ngOnInit(): void {
    // Sử dụng ActivatedRoute để lấy giá trị của tham số cityName từ URL
    this.activatedRoute.paramMap.subscribe(params => {
      this.cityName = params.get('cityName');
      // Bây giờ, bạn có thể sử dụng this.cityName trong component của bạn
    });

    // lấy thông tin thành phố mà người dùng chọn
    this.subcription = this.sharedService.passSubject$.subscribe(data => {
      console.log(data);
    })
  }


  
  ngOnDestroy() {
    this.subcription?.unsubscribe();
  }
}

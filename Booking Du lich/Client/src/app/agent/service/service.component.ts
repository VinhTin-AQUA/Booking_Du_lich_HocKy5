import { Component } from '@angular/core';
import { HotelService } from 'src/app/shared/models/hotelService/hotelService';
import { AgentService } from '../agent.service';
import { SharedService } from 'src/app/shared/shared.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-service',
  templateUrl: './service.component.html',
  styleUrls: ['./service.component.scss'],
})
export class ServiceComponent {
  newService: string[] = []; // danh sách mới hiển thị cho người dùng
  oldService: HotelService[] = []; // danh sach service cũ
  hotelID: number | null = null;

  addServiceForm: FormGroup = new FormGroup([]);
  submitted: boolean = false;

  isShowAddService: boolean = false;

  constructor(
    private agentService: AgentService,
    private formBuilder: FormBuilder,
    private sharedService: SharedService,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit() {
    this.addServiceForm = this.formBuilder.group({
      serviceName: ['', Validators.required],
    });

    this.activatedRoute.params.subscribe({
      next: (params: any) => {
        if (params['id'] !== undefined) {
          this.hotelID = params['id'];
        }
        
        //this.resetHotelForm();
      },
    });
  }

  private getAllServices() {
    
  }

  showAddService() {
    this.isShowAddService = !this.isShowAddService;
  }

  addServiceSubmit() {
    this.submitted = true;

    if (this.addServiceForm.valid) {
      this.sharedService.showLoading(true);
      console.log(this.addServiceForm.value);
      this.agentService
        .addNewService(this.addServiceForm.value.serviceName)
        .subscribe({
          next: (res: any) => {
            this.sharedService.showLoading(false);
            this.sharedService.showToastMessage(
              'successThêm service thành công'
            );
            this.isShowAddService = false;
          },
          error: (err) => {
            this.sharedService.showToastMessage(err.error.Value.message);
            this.sharedService.showLoading(false);
          },
        });
    }
  }
}

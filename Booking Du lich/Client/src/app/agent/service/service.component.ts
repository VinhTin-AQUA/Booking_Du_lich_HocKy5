import { Component } from '@angular/core';
import { HotelService } from 'src/app/shared/models/hotelService/hotelService';
import { AgentService } from '../agent.service';
import { SharedService } from 'src/app/shared/shared.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { HasService } from 'src/app/shared/models/hotelService/hasService';

@Component({
  selector: 'app-service',
  templateUrl: './service.component.html',
  styleUrls: ['./service.component.scss'],
})
export class ServiceComponent {
  systemServices: HotelService[] = []; // danh sach service của ứng dụng
  newServics: string[] = []; // danh sách mới hiển thị cho người dùng
  oldServices: HasService[] = []; // danh sách services của hotel đã co
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

    this.getAllServices();
    this.getServicesOfHotel();
  }

  private getAllServices() {
    this.agentService.getAllServices().subscribe({
      next: (res: any) => {
        this.systemServices = res;
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  private getServicesOfHotel() {
    if (this.hotelID !== null) {
      this.agentService.getServicesOfHotel(this.hotelID).subscribe({
        next: (res: any) => {
          this.oldServices = res.Value.HasServices;
        },
        error: (err) => {
          console.log(err);
        },
      });
    }
  }

  private resetForm() {
    this.addServiceForm.controls['serviceName'].setValue('');
    this.submitted = false;
  }

  private resetSystemService(service: HotelService) {
    this.systemServices.push(service);
  }

  private resetAddOldService(hasService: HasService) {
    this.oldServices.push(hasService);
  }

  private resetDeleteOldService(serviceId: number) {
    if (this.hotelID !== null) {
      const index = this.oldServices.findIndex(
        (hs) => hs.HotelID == this.hotelID && hs.ServiceID == serviceId
      );
      if (index !== -1) {
        this.oldServices.splice(index, 1);
      }
    }
  }

  showAddService() {
    this.isShowAddService = !this.isShowAddService;
    if (this.isShowAddService === false) {
      this.resetForm();
    }
  }

  addServiceSubmit() {
    this.submitted = true;
    if (this.addServiceForm.valid) {
      this.sharedService.showLoading(true);
      this.agentService
        .addNewService(this.addServiceForm.value.serviceName)
        .subscribe({
          next: (res: any) => {
            this.sharedService.showLoading(false);
            this.sharedService.showToastMessage(
              'successThêm service thành công'
            );
            this.isShowAddService = false;
            this.resetForm();
            this.resetSystemService(res.Value.newHotelService);
          },
          error: (err) => {
            this.sharedService.showToastMessage(err.error.Value.message);
            this.sharedService.showLoading(false);
          },
        });
    }
  }

  addServiceToHotel(ServiceID: number) {
    if (this.hotelID !== null) {
      let form = new FormData();
      form.append('HotelID', this.hotelID.toString());
      form.append('ServiceID', ServiceID.toString());

      this.agentService.addServiceToHotel(form).subscribe({
        next: (res: any) => {
          this.sharedService.showToastMessage('successThêm dịch vụ thành công');
          this.resetAddOldService(res.Value.HasService);
        },
        error: (err) => {
          this.sharedService.showToastMessage(
            'Dịch vụ này đã có trong khách sạn'
          );
        },
      });
    }
  }

  removeServiceToHotel(serviceId: number) {
    if (this.hotelID !== null) {
      this.agentService.deleteServiceHotel(this.hotelID, serviceId).subscribe({
        next: (_) => {
          this.sharedService.showToastMessage('successXóa thành công');
          this.resetDeleteOldService(serviceId);
        },
        error: (_) => {
          this.sharedService.showToastMessage('Lỗi. Hãy thử lại');
        },
      });
    }
  }
}

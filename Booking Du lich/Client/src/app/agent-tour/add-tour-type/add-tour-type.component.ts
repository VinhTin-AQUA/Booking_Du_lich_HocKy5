import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { TourType } from 'src/app/shared/models/tour/tourType';
import { map, mergeMap } from 'rxjs';
import { AgentTourService } from '../agent-tour.service';
import { Tour } from 'src/app/shared/models/tour/tour';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-add-tour-type',
  templateUrl: './add-tour-type.component.html',
  styleUrls: ['./add-tour-type.component.scss'],
})
export class AddTourTypeComponent {
  tour: Tour | null = null;
  sysTourTypes: TourType[] = [];
  isShowAddTourType: boolean = false;
  addTourTypeForm: FormGroup = new FormGroup([]);
  submitted: boolean = false;

  constructor(
    private activatedRoute: ActivatedRoute,
    private agentTourService: AgentTourService,
    private sharedService: SharedService,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit() {
    this.activatedRoute.params
      .pipe(
        map((tourId) => {
          return tourId['id'];
        }),
        mergeMap((tourId) => this.agentTourService.getTourById(tourId))
      )
      .subscribe({
        next: (res: any) => {
          if (res !== null) {
            this.tour = res.tour;
          }
          console.log();
          
        },
        error: (err) => {
          console.log(err);
        },
      });

    this.getAllTourTypes();

    this.addTourTypeForm = this.formBuilder.group({
      tourTypeName: ['', [Validators.required]],
    });
  }

  private getAllTourTypes() {
    this.agentTourService.getAllTourTypes().subscribe((tourTypes: any) => {
      this.sysTourTypes = tourTypes;
    });
  }

  resetAddTourTypeGroup() {
    this.submitted = false;
    this.addTourTypeForm.controls['tourTypeName'].setValue('');
  }

  removeTourTypeToTour(tourTypeId: number) {}

  showAddTourType() {
    this.isShowAddTourType = !this.isShowAddTourType;
    if (this.isShowAddTourType === false) {
      this.resetAddTourTypeGroup();
    }
  }

  addTourTypeSubmit() {
    this.submitted = true;

    if (this.addTourTypeForm.valid) {
      let form = new FormData();
      form.append('TourTypeName', this.addTourTypeForm.value.tourTypeName);
      this.agentTourService.addTourType(form).subscribe({
        next: (res: any) => {
          this.sharedService.showToastMessage(
            'successThêm loại tour thành công'
          );
          this.sysTourTypes.push(res.Value.newTourType);
          this.resetAddTourTypeGroup();
        },
        error: (err) => {
          console.log(err);
          this.sharedService.showToastMessage(err.error.Value.message);
        },
      });
    }
  }

  addTypeToTour(tourType: TourType | null) {
    if (this.tour !== null && tourType !== null) {
      this.sharedService.showLoading(true);
      this.agentTourService
        .addTypeToTour(tourType.TourTypeId, this.tour?.TourId)
        .subscribe({
          next: (_) => {
            this.sharedService.showToastMessage(
              'successThêm tour type thành công'
            );
            if (this.tour !== null) {
              this.tour.TourType = tourType;
            }
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

import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AddTourType } from 'src/app/shared/models/tour/addTourType';
import { AdminService } from '../admin.service';
import { TourType } from 'src/app/shared/models/tour/tourType';
import { SharedService } from 'src/app/shared/shared.service';
import { UpdateTourType } from 'src/app/shared/models/tour/updateTourType';

@Component({
  selector: 'app-manage-tour-type',
  templateUrl: './manage-tour-type.component.html',
  styleUrls: ['./manage-tour-type.component.scss'],
})
export class ManageTourTypeComponent {
  addFormSubmit: FormGroup = new FormGroup([]);
  submitted: boolean = false;
  errorMessages: string[] = [];
  tourTypes: TourType[] = [];

  isDeleteModal: boolean = false;
  tourTypeIdToDelete: number | null = null;

  updateFormSubmit: FormGroup = new FormGroup([]);
  isUpdateModal: boolean = false;
  tourTypeUpdate: UpdateTourType | null = null;

  constructor(
    private formbuilder: FormBuilder,
    private adminService: AdminService,
    private sharedService: SharedService
  ) {}

  ngOnInit() {
    this.addFormSubmit = this.formbuilder.group({
      tourTypeName: ['', [Validators.required]],
    });

    this.updateFormSubmit = this.formbuilder.group({
      tourTypeId: ['', [Validators.required]],
      tourTypeName: ['', [Validators.required]],
    });

    this.adminService.getAllTours().subscribe({
      next: (res: any) => {
        this.tourTypes = res;
      },
    });
  }

  inputValidation(field: string) {
    if (this.submitted === true) {
      if (this.addFormSubmit.hasError('required', field)) {
        return 'input-filed-error';
      }
    }
    return '';
  }

  private addTourToView(tourType: TourType) {
    this.tourTypes.push(tourType);
  }

  private deleteTourToView(tourTypeId: number | null) {
    this.isDeleteModal = false;
    const index = this.tourTypes.findIndex((x) => x.TourTypeId === tourTypeId);
    if (index !== -1) {
      this.tourTypes.splice(index, 1);
    }
  }

  private updateTourToView(tourTypeName: string) {
    let tourType = this.tourTypes.find(x => x.TourTypeId === this.tourTypeUpdate?.TourTypeId);
    if (tourType !== null && tourType !== undefined) {
      tourType.TourTypeName = tourTypeName;
    }
  }

  addSubmit() {
    this.submitted = true;
    this.errorMessages = [];

    if (this.addFormSubmit.valid) {
      const addTourType: AddTourType = {
        TourTypeName: this.addFormSubmit.value.tourTypeName,
      };
      this.sharedService.showLoading(true);
      this.adminService.addTourType(addTourType).subscribe({
        next: (res: any) => {
          this.sharedService.showLoading(false);
          this.sharedService.showToastMessage('success' + res.Value.message);
          this.addTourToView(res.Value.newTourType);
        },
        error: (err) => {
          this.sharedService.showLoading(false);
        },
      });
    }
  }

  showDeleteModal(tourTypeId: number | null) {
    this.isDeleteModal = !this.isDeleteModal;
    this.tourTypeIdToDelete = tourTypeId;
  }

  deleteTourTypeSubmit() {
    if (this.tourTypeIdToDelete !== null) {
      this.sharedService.showLoading(true);
      this.adminService.deleteTourType(this.tourTypeIdToDelete).subscribe({
        next: (res: any) => {
          this.sharedService.showLoading(false);
          this.sharedService.showToastMessage('success' + res.Value.message);
          this.deleteTourToView(this.tourTypeIdToDelete);
        },
        error: (err) => {
          this.sharedService.showLoading(false);
          this.sharedService.showToastMessage(err.error.Value.message);
        },
      });
    }
  }

  private resetUpdateFormSubmit() {
    if (this.tourTypeUpdate === null) {
      this.updateFormSubmit.controls['tourTypeId'].setValue('')
      this.updateFormSubmit.controls['tourTypeName'].setValue('')
    } else {
      this.updateFormSubmit.controls['tourTypeId'].setValue(this.tourTypeUpdate.TourTypeId)
      this.updateFormSubmit.controls['tourTypeName'].setValue(this.tourTypeUpdate.TourTypeName)
    }
  }

  showUpdateTourTypeModal(tourType: TourType | null) {
    this.isUpdateModal = !this.isUpdateModal;
    this.tourTypeUpdate = tourType;
    this.resetUpdateFormSubmit();
  }

  updateTourTypeSubmit() {
    if (this.updateFormSubmit.valid && this.tourTypeUpdate !== null) {
      this.sharedService.showLoading(true);

      this.adminService.updateTourType(this.updateFormSubmit.value).subscribe({
        next: (_) => {
          this.updateTourToView(this.updateFormSubmit.value.tourTypeName);
          this.showUpdateTourTypeModal(null);
          this.sharedService.showLoading(false);
          this.sharedService.showToastMessage('successUpdate tourtype successfully');
        },
        error: (err) => {
          this.sharedService.showLoading(false);
          this.sharedService.showToastMessage(err.error.Value.message);
          this.showUpdateTourTypeModal(null);
        }
      })
    }
  }
}

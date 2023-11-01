import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RoomType } from 'src/app/shared/models/room/roomType';
import { SharedService } from 'src/app/shared/shared.service';
import { AgentService } from '../agent.service';

@Component({
  selector: 'app-manage-room-type',
  templateUrl: './manage-room-type.component.html',
  styleUrls: ['./manage-room-type.component.scss'],
})
export class ManageRoomTypeComponent {
  isUpdateModal: boolean = false;
  roomTypes: RoomType[] = [];
  tourTypeUpdate: RoomType | null = null;
  submitted: boolean = false;

  addFormSubmit: FormGroup = new FormGroup([]);
  updateFormSubmit: FormGroup = new FormGroup([]);
  isDeleteModal: boolean = false;
  roomTypeIdToDelete: number | null = null;

  constructor(
    private formbuilder: FormBuilder,
    private sharedService: SharedService,
    private agentService: AgentService
  ) {
    this.addFormSubmit = this.formbuilder.group({
      roomTypeName: ['', [Validators.required]],
    });

    this.updateFormSubmit = this.formbuilder.group({
      roomTypeId: ['', [Validators.required]],
      roomTypeName: ['', [Validators.required]],
    });

    this.getAllRoomTypes();
  }

  inputValidation(field: string) {
    if (this.submitted === true) {
      if (this.addFormSubmit.hasError('required', field)) {
        return 'input-filed-error';
      }
    }
    return '';
  }

  showDeleteModal(tourTypeId: number | null) {
    this.isDeleteModal = !this.isDeleteModal;
    this.roomTypeIdToDelete = tourTypeId;
  }

  showUpdateRoomTypeModal(roomType: RoomType | null) {
    this.isUpdateModal = !this.isUpdateModal;
    this.tourTypeUpdate = roomType;
    this.resetUpdateFormSubmit();
  }

  private resetUpdateFormSubmit() {
    if (this.tourTypeUpdate === null) {
      this.updateFormSubmit.controls['tourTypeId'].setValue('');
      this.updateFormSubmit.controls['tourTypeName'].setValue('');
    } else {
      this.updateFormSubmit.controls['tourTypeId'].setValue(
        this.tourTypeUpdate.RoomTypeId
      );
      this.updateFormSubmit.controls['tourTypeName'].setValue(
        this.tourTypeUpdate.RoomTypeName
      );
    }
  }

  private addRoomTypeToView(roomType: RoomType) {
    this.roomTypes.push(roomType);
  }

  private getAllRoomTypes() {
    this.agentService.getAllRoomType().subscribe((rt: any) => {
      this.roomTypes = rt.Value.RoomTypes;
    })
  }

  addSubmit() {
    this.submitted = true;

    if (this.addFormSubmit.valid) {
      let form: FormData = new FormData();
      form.append('RoomTypeName', this.addFormSubmit.value.roomTypeName);

      this.sharedService.showLoading(true);

      this.agentService.addRoomType(form).subscribe({
        next: (res: any) => {
          this.sharedService.showLoading(false);
          this.sharedService.showToastMessage('success' + res.Value.message);
          this.addRoomTypeToView(res.Value.RoomType);
        },
        error: (err) => {
          this.sharedService.showLoading(false);
          console.log(err);
          
        },
      });
    }
  }

  deleteTourTypeSubmit() {}

  updateTourTypeSubmit() {}
}

import { Component, ElementRef, ViewChild } from '@angular/core';
import { ImgShow } from 'src/app/shared/models/image/imgShow';

@Component({
  selector: 'app-add-tour',
  templateUrl: './add-tour.component.html',
  styleUrls: ['./add-tour.component.scss']
})
export class AddTourComponent {
  //img
  listNewImgUrls: ImgShow[] = []; // danh sách ảnh mới để hiển thị trên view
  newImgObjToAdd: any = []; // danh sách các đối tượng file gửi lên server
  @ViewChild('fileInput') fileInput!: ElementRef;
  
  private clearFile() {
    this.fileInput.nativeElement.value = '';
  }

  onSelectImg(event: any) {
    for (let file of event.target.files) {
      this.newImgObjToAdd.push(file);
    }

    if (event.target.files) {
      const n = event.target.files.length;
      for (let i = 0; i < n; i++) {
        var reader = new FileReader();
        reader.readAsDataURL(event.target.files[i]);

        let imgShow: ImgShow = {
          name: event.target.files[i].name,
          data: '',
        };

        reader.onload = (events: any) => {
          imgShow.data = events.target.result;
          this.listNewImgUrls.push(imgShow);
        };
      }
      this.clearFile();
    }
  }
}



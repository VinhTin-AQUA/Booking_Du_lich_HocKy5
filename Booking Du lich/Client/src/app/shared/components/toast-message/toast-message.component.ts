import { Component, OnDestroy, OnInit } from '@angular/core';
import { SharedService } from '../../shared.service';

@Component({
  selector: 'app-toast-message',
  templateUrl: './toast-message.component.html',
  styleUrls: ['./toast-message.component.scss'],
})
export class ToastMessageComponent implements OnInit {
  message: string = '';
  isSuccess: boolean = true;
  _classToast: string = '';
  _classCloseBtn: string = '';
  timeout: any;

  constructor(public sharedService: SharedService) {}

  ngOnInit(): void {
    this.sharedService.showToastMessage$.subscribe((message) => {
      if (message !== '') {
        message.startsWith('success')
          ? ((this.isSuccess = true),
            (this._classToast = 'bg-green-500'),
            (this._classCloseBtn = 'hover:bg-green-800'))
          : ((this.isSuccess = false),
            (this._classToast = 'bg-red-500'),
            (this._classCloseBtn = 'hover:bg-red-800'));
        this.message = message.substring(7);
        this.timeOut();
      }
    });
  }

  timeOut() {
    this.timeout = setTimeout(() => {
      this.message = '';
    }, 3800);
  }

  close() {
    this.message = '';
    clearTimeout(this.timeout);
  }
}

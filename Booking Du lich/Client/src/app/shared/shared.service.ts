import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SharedService {
  private isLoading: ReplaySubject<boolean | null> = new ReplaySubject(1);
  private isToastMessage: ReplaySubject<string> = new ReplaySubject(1);

  isLoading$ = this.isLoading.asObservable();
  showToastMessage$ = this.isToastMessage.asObservable();

  passSubject: Subject<any> = new Subject<any>();

  constructor() {}

  showLoading(isShowed: boolean) {
    this.isLoading.next(isShowed);
  }

  showToastMessage(message: string) {
    this.isToastMessage.next(message);
  }

  passObj(obj: any) {
    this.passSubject.next(obj);
  }
}

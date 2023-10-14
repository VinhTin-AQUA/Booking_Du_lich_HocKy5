import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { Subject,BehaviorSubject } from 'rxjs';
import { Hotel } from './models/hotel/hotel';

@Injectable({
  providedIn: 'root',
})
export class SharedService {
  private isLoading: ReplaySubject<boolean | null> = new ReplaySubject(1);
  isLoading$ = this.isLoading.asObservable();

  private isToastMessage: ReplaySubject<string> = new ReplaySubject(1);
  showToastMessage$ = this.isToastMessage.asObservable();

  private passSubject = new BehaviorSubject<any>(null);
  passSubject$ =this.passSubject.asObservable();

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

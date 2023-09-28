import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SharedService {
  private isLoading: ReplaySubject<boolean | null> = new ReplaySubject(1);
  isLoading$ = this.isLoading.asObservable();

  constructor() {}

  showLoading(isShowed: boolean) {
    this.isLoading.next(isShowed);
  }
}

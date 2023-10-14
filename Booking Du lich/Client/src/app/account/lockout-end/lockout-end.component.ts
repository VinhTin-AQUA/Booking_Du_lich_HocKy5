import { Component, OnInit } from '@angular/core';
import {Subscription} from 'rxjs'
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-lockout-end',
  templateUrl: './lockout-end.component.html',
  styleUrls: ['./lockout-end.component.scss']
})
export class LockoutEndComponent implements OnInit {
  message: string = ''
  private subcription: Subscription | null = null;

  constructor(private sharedService: SharedService) {

  }

  ngOnInit(): void {
    this.subcription = this.sharedService.passSubject$.subscribe(message => {
      this.message = message
    })
  }

  ngOndestroy() {
    this.subcription?.unsubscribe();
  }
}

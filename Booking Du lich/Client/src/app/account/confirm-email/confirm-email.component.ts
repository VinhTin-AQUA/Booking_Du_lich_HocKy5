import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { AccountService } from '../account.service';
import { ConfirmEmail } from 'src/app/shared/models/account/confirmEmail';

@Component({
  selector: 'app-confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrls: ['./confirm-email.component.scss']
})
export class ConfirmEmailComponent implements OnInit {
  message: string  = '';

  constructor(private accountService: AccountService,
    private activatedRoute: ActivatedRoute,
    private router: Router) {

  }

  ngOnInit(): void {
    this.activatedRoute.queryParamMap.subscribe({
      next: (params: any) => {
        const model: ConfirmEmail = {
          token: params.get('token'), 
          email: params.get('email')
        }
        
        this.accountService.confirmEmail(model).subscribe({
          next: (res: any) => {
            
          },
          error: (err: any) => {
            console.log(err.error);
            
          }
        })
      } 
    })
  }


}

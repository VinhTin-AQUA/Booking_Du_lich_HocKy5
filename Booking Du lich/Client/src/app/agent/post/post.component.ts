import { state } from '@angular/animations';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Hotel } from 'src/app/shared/models/hotel/hotel';
import { AgentService } from '../agent.service';
import { AccountService } from 'src/app/account/account.service';
import { map, mergeMap } from 'rxjs';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.scss'],
})
export class PostComponent {
  hotels: Hotel[] = [];
  userId: string | null = null;

  constructor(
    private router: Router,
    private agentService: AgentService,
    private accountService: AccountService
  ) {}


  ngOnInit() {
    this.getHotelsOfAgent();
  }


  private getHotelsOfAgent() {

    this.accountService.user$.pipe(
      map(u => {
        return u
      }),
      mergeMap(u => this.agentService.getHotelsOfAgentHotel(u?.Id)))
      .subscribe({
        next: (res:any) => {
          console.log(res);
          
        },
        error: (err) => {
          console.log(err);
          
        }
      })


    
  }
}

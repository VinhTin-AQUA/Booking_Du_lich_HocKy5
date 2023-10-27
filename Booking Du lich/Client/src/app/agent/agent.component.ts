import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-agent',
  templateUrl: './agent.component.html',
  styleUrls: ['./agent.component.scss']
})
export class AgentComponent {
  constructor(private router: Router) {
    this.router.navigateByUrl('/agent/hotel')
  }
}

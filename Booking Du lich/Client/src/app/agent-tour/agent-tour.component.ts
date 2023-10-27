import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-agent-tour',
  templateUrl: './agent-tour.component.html',
  styleUrls: ['./agent-tour.component.scss']
})
export class AgentTourComponent {
  constructor(private router: Router) {
    router.navigateByUrl('/agent-tour/add-tour')
  }
}

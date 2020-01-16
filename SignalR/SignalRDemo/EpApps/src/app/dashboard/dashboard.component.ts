import { Component } from '@angular/core';
import { IEpViewModel } from '@epicor/kinetic';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent {
  viewModel: IEpViewModel = {
    contextBar: {
      title: 'Dashboard view.'
    }
  };
}

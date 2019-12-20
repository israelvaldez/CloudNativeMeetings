import { Component, OnInit } from '@angular/core';
import { IEpViewModel } from '@epicor/kinetic';
import { AppCommonService } from '../app.common.service';
import { AppLocalizationStrings } from '../app.localization.strings';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.scss']
})
export class AboutComponent implements OnInit {
  viewModel: IEpViewModel;
  appStrings = AppLocalizationStrings;

  constructor(private appCommonService: AppCommonService) {
    this.viewModel = this.appCommonService.getViewModel();
    this.viewModel.contextBar.title = this.appCommonService.appStrings.about;
  }

  ngOnInit() {}
}

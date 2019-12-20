import { Component, OnInit } from '@angular/core';
import { AppLocalizationStrings } from '../app.localization.strings';

@Component({
  selector: 'app-left-sidebar',
  templateUrl: './left-sidebar.component.html',
  styleUrls: ['./left-sidebar.component.scss']
})
export class LeftSidebarComponent implements OnInit {
  appStrings = AppLocalizationStrings;

  constructor() {}

  ngOnInit() {}
}

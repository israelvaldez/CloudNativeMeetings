import { Component, OnInit, AfterViewInit } from '@angular/core';
import { IEpViewModel } from '@epicor/kinetic';
import { AppCommonService } from '../app.common.service';
import * as signalR from '@aspnet/signalr';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit, AfterViewInit {

  viewModel: IEpViewModel;
  private connection: signalR.HubConnection;
  color = 'blue';

  constructor(private appCommonService: AppCommonService) {
    this.viewModel = this.appCommonService.getViewModel();
  }

  ngOnInit() {

  }

  ngAfterViewInit(): void {

    this.connection = new signalR.HubConnectionBuilder()
      .configureLogging(signalR.LogLevel.Debug)
      .withUrl('https://localhost:44305/eventHub').build();

    this.connection.on('SendNoticeEventToClient', (message) => {
      this.color = message;
    });

    this.connection.start().then(function () {
      console.log('SignalR is now connected.');
    }).catch(function (err) {
      return console.error(err.toString());
    });
  }


}

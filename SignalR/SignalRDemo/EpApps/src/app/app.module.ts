import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';

import {
  EpCore2Module,
  EpLoginComponent,
  EpLocalizationService,
  EpApplicationModule,
  EpShellModule,
  EpLoginModule,
  EpEulaModule
} from '@epicor/kinetic';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { LeftSidebarComponent } from './left-sidebar/left-sidebar.component';
import { AppLocalizationStrings } from './app.localization.strings';
import { AboutComponent } from './about/about.component';
import { AppCommonService } from './app.common.service';
import { DashboardComponent } from './dashboard/dashboard.component';

/**
 * Register the application localization strings to be translated.
 */
EpLocalizationService.registerStrings(AppLocalizationStrings);

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LeftSidebarComponent,
    AboutComponent,
    DashboardComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    FormsModule,
    EpLoginModule,
    EpShellModule,
    EpApplicationModule,
    EpEulaModule,
    EpCore2Module.forRoot()
  ],
  providers: [AppCommonService],
  bootstrap: [AppComponent],
  entryComponents: [
    EpLoginComponent,
    LeftSidebarComponent
  ]
})
export class AppModule {}

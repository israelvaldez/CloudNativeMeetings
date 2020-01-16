import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { EpCore2Module, EpShellModule } from '@epicor/kinetic';

import { HomeComponent } from './home.component';
import { AppCommonService } from '../app.common.service';
import { LeftSidebarComponent } from '../left-sidebar/left-sidebar.component';
import { BrowserDynamicTestingModule } from '@angular/platform-browser-dynamic/testing';

describe('HomeComponent', () => {
  let component: HomeComponent;
  let fixture: ComponentFixture<HomeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [EpCore2Module.forRoot(), RouterTestingModule, EpShellModule],
      providers: [AppCommonService],
      declarations: [HomeComponent, LeftSidebarComponent]
    });

    TestBed.overrideModule(BrowserDynamicTestingModule, {
      set: {
        entryComponents: [LeftSidebarComponent]
      }
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

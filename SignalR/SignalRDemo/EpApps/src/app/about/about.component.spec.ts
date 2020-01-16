import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AboutComponent } from './about.component';
import { EpCore2Module, EpShellModule } from '@epicor/kinetic';
import { AppCommonService } from '../app.common.service';
import { LeftSidebarComponent } from '../left-sidebar/left-sidebar.component';
import { BrowserDynamicTestingModule } from '@angular/platform-browser-dynamic/testing';

describe('AboutComponent', () => {
  let component: AboutComponent;
  let fixture: ComponentFixture<AboutComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [EpCore2Module.forRoot(), RouterTestingModule, EpShellModule],
      providers: [AppCommonService],
      declarations: [AboutComponent, LeftSidebarComponent]
    });

    TestBed.overrideModule(BrowserDynamicTestingModule, {
      set: {
        entryComponents: [LeftSidebarComponent]
      }
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AboutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

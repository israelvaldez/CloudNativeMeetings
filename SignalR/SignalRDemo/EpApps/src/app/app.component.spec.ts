import { TestBed, async } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AppComponent } from './app.component';
import {
  EpCore2Module,
  EpApplicationModule,
  EpShellModule
} from '@epicor/kinetic';
import { AppCommonService } from './app.common.service';
describe('AppComponent', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        EpCore2Module.forRoot(),
        EpApplicationModule,
        EpShellModule
      ],
      declarations: [AppComponent],
      providers: [AppCommonService]
    }).compileComponents();
  }));
  it('should create the app', async(() => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app).toBeTruthy();
  }));
});

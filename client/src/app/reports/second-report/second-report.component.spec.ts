import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SecondReportComponent } from './second-report.component';

describe('SecondReportComponent', () => {
  let component: SecondReportComponent;
  let fixture: ComponentFixture<SecondReportComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SecondReportComponent]
    });
    fixture = TestBed.createComponent(SecondReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

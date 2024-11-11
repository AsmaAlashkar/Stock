import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GenralReportComponent } from './genral-report.component';

describe('GenralReportComponent', () => {
  let component: GenralReportComponent;
  let fixture: ComponentFixture<GenralReportComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [GenralReportComponent]
    });
    fixture = TestBed.createComponent(GenralReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

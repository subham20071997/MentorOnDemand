import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentnotificationComponent } from './studentnotification.component';

describe('StudentnotificationComponent', () => {
  let component: StudentnotificationComponent;
  let fixture: ComponentFixture<StudentnotificationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StudentnotificationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StudentnotificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MentornotificationComponent } from './mentornotification.component';

describe('MentornotificationComponent', () => {
  let component: MentornotificationComponent;
  let fixture: ComponentFixture<MentornotificationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MentornotificationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MentornotificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

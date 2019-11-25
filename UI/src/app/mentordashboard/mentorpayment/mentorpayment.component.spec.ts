import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MentorpaymentComponent } from './mentorpayment.component';

describe('MentorpaymentComponent', () => {
  let component: MentorpaymentComponent;
  let fixture: ComponentFixture<MentorpaymentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MentorpaymentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MentorpaymentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

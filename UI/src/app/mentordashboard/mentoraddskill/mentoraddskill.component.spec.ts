import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MentoraddskillComponent } from './mentoraddskill.component';

describe('MentoraddskillComponent', () => {
  let component: MentoraddskillComponent;
  let fixture: ComponentFixture<MentoraddskillComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MentoraddskillComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MentoraddskillComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

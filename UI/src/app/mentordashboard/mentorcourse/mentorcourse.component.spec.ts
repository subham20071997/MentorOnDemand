import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MentorcourseComponent } from './mentorcourse.component';

describe('MentorcourseComponent', () => {
  let component: MentorcourseComponent;
  let fixture: ComponentFixture<MentorcourseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MentorcourseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MentorcourseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

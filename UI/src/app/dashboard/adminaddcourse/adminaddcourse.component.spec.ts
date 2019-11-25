import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminaddcourseComponent } from './adminaddcourse.component';

describe('AdminaddcourseComponent', () => {
  let component: AdminaddcourseComponent;
  let fixture: ComponentFixture<AdminaddcourseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdminaddcourseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminaddcourseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

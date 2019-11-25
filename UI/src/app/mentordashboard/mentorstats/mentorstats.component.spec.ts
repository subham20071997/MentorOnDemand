import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MentorstatsComponent } from './mentorstats.component';

describe('MentorstatsComponent', () => {
  let component: MentorstatsComponent;
  let fixture: ComponentFixture<MentorstatsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MentorstatsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MentorstatsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

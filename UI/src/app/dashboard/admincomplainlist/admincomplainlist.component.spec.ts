import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdmincomplainlistComponent } from './admincomplainlist.component';

describe('AdmincomplainlistComponent', () => {
  let component: AdmincomplainlistComponent;
  let fixture: ComponentFixture<AdmincomplainlistComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdmincomplainlistComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdmincomplainlistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

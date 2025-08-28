import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditMenuItem } from './edit-menu-item';

describe('EditMenuItem', () => {
  let component: EditMenuItem;
  let fixture: ComponentFixture<EditMenuItem>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditMenuItem]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditMenuItem);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

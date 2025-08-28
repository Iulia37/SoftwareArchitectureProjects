import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateMenuItem } from './create-menu-item';

describe('CreateMenuItem', () => {
  let component: CreateMenuItem;
  let fixture: ComponentFixture<CreateMenuItem>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateMenuItem]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateMenuItem);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

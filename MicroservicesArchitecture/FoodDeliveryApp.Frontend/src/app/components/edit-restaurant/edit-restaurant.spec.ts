import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditRestaurant } from './edit-restaurant';

describe('EditRestaurant', () => {
  let component: EditRestaurant;
  let fixture: ComponentFixture<EditRestaurant>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditRestaurant]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditRestaurant);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

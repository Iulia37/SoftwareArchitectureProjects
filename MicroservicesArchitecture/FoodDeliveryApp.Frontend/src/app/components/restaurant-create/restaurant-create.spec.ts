import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RestaurantCreate } from './restaurant-create';

describe('RestaurantCreate', () => {
  let component: RestaurantCreate;
  let fixture: ComponentFixture<RestaurantCreate>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RestaurantCreate]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RestaurantCreate);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

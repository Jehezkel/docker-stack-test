import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GenericCRUDComponent } from './generic-crud.component';

describe('GenericCRUDComponent', () => {
  let component: GenericCRUDComponent;
  let fixture: ComponentFixture<GenericCRUDComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GenericCRUDComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GenericCRUDComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

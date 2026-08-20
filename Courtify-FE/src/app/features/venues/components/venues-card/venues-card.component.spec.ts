import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VenuesCardComponent } from './venues-card.component';

describe('VenuesCardComponent', () => {
  let component: VenuesCardComponent;
  let fixture: ComponentFixture<VenuesCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VenuesCardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(VenuesCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

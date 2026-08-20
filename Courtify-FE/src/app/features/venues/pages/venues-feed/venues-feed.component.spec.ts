import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VenuesFeedComponent } from './venues-feed.component';

describe('VenuesFeedComponent', () => {
  let component: VenuesFeedComponent;
  let fixture: ComponentFixture<VenuesFeedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VenuesFeedComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(VenuesFeedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

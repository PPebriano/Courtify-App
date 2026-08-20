import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VenueDetailBannerComponent } from './venue-detail-banner.component';

describe('VenueDetailBannerComponent', () => {
  let component: VenueDetailBannerComponent;
  let fixture: ComponentFixture<VenueDetailBannerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VenueDetailBannerComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(VenueDetailBannerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

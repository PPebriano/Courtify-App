import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookingFeedComponent } from './booking-feed.component';

describe('BookingFeedComponent', () => {
  let component: BookingFeedComponent;
  let fixture: ComponentFixture<BookingFeedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BookingFeedComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BookingFeedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

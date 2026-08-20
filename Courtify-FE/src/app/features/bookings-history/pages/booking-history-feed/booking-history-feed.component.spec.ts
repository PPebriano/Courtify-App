import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookingHistoryFeedComponent } from './booking-history-feed.component';

describe('BookingHistoryFeedComponent', () => {
  let component: BookingHistoryFeedComponent;
  let fixture: ComponentFixture<BookingHistoryFeedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BookingHistoryFeedComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BookingHistoryFeedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

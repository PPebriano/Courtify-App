import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookingHistoryDetailComponent } from './booking-history-detail.component';

describe('BookingHistoryDetailComponent', () => {
  let component: BookingHistoryDetailComponent;
  let fixture: ComponentFixture<BookingHistoryDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BookingHistoryDetailComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BookingHistoryDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

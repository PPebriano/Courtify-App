import { Component, inject } from '@angular/core';
import { BannerComponent } from '../../../../shared/components/banner/banner.component';
import { BookingsService } from '../../../../core/services/bookings.service';
import { BookingResponseType } from '../../../../core/models/response/booking-response-type';

@Component({
  selector: 'app-booking-feed',
  standalone: true,
  imports: [BannerComponent],
  templateUrl: './booking-feed.component.html',
  styleUrl: './booking-feed.component.css',
})
export class BookingFeedComponent {
  private bookings = inject(BookingsService);

  activeBookings: BookingResponseType[] = [];

  ngOnInit() {
    this.fetchBookingHistory;
  }

  private fetchBookingHistory() {
    const dummyValue = [
      {
        id: 1,
        bookingCode: 'BK-220204',
        adminId: 1,
        courtId: 1,
        customerName: 'Dadang Gunawan',
        bookingDate: '22-02-04',
        startTime: '14:00',
        endTime: '18:00',
        total_hours: 4,
        base_amount: 4000000,
        grandTotal: 3500000,
        status: 'ACTIVE',
        createdAt: '22-Februari-2004 14:40:39z',
      },
    ];

    this.activeBookings = dummyValue;

    this.bookings.bookings().subscribe({
      next: (response) => {
        console.log(response);
        this.activeBookings = response;
      },
    });
  }
}

import { APP_ROUTES } from './../../../../shared/constants/routes';
import { BookingCardComponent } from './../../../../shared/components/booking-card/booking-card.component';
import { Component, inject } from '@angular/core';
import { BannerComponent } from '../../../../shared/components/banner/banner.component';
import { BookingsService } from '../../../../core/services/bookings.service';
import { BookingResponseType } from '../../../../core/models/response/booking-response-type';

@Component({
  selector: 'app-booking-history-feed',
  standalone: true,
  imports: [BannerComponent, BookingCardComponent],
  templateUrl: './booking-history-feed.component.html',
  styleUrl: './booking-history-feed.component.css',
})
export class BookingHistoryFeedComponent {
  private bookings = inject(BookingsService);
  readonly APP_ROUTES = APP_ROUTES;

  bookingHistories: BookingResponseType[] = [];

  ngOnInit() {
    this.fetchBookingHistories();
  }

  private fetchBookingHistories() {
    // const dummyValue = [
    //   {
    //     id: 1,
    //     bookingCode: 'BK-220204',
    //     adminId: 1,
    //     courtId: 1,
    //     customerName: 'Dadang Gunawan',
    //     bookingDate: '22-02-04',
    //     startTime: '14:00',
    //     endTime: '18:00',
    //     totalHours: 4,
    //     baseAmount: 4000000,
    //     totalAmount: 3500000,
    //     status: 'DONE',
    //     createdAt: '22-Februari-2004',
    //   },
    // ];

    // this.bookingHistories = dummyValue;

    this.bookings.bookings('DONE').subscribe({
      next: (response) => {
        console.log(response);
        this.bookingHistories = response;
      },
    });
  }
}

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
    this.bookings.bookings('DONE').subscribe({
      next: (response) => {
        console.log(response);
        this.bookingHistories = response;
      },
    });
  }
}

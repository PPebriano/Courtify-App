import { APP_ROUTES } from './../../../../shared/constants/routes';
import { Component, inject } from '@angular/core';
import { BannerComponent } from '../../../../shared/components/banner/banner.component';
import { BookingsService } from '../../../../core/services/bookings.service';
import { BookingResponseType } from '../../../../core/models/response/booking-response-type';
import { BookingCardComponent } from '../../../../shared/components/booking-card/booking-card.component';

@Component({
  selector: 'app-booking-feed',
  standalone: true,
  imports: [BannerComponent, BookingCardComponent],
  templateUrl: './booking-feed.component.html',
  styleUrl: './booking-feed.component.css',
})
export class BookingFeedComponent {
  private bookings = inject(BookingsService);
  readonly APP_ROUTES = APP_ROUTES;

  activeBookings: BookingResponseType[] = [];

  ngOnInit() {
    this.fetchBookingHistory();
  }

  private fetchBookingHistory() {
    this.bookings.bookings('ACTIVE').subscribe({
      next: (response) => {
        console.log(response);
        this.activeBookings = response;
      },
    });
  }
}

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
    //     status: 'ACTIVE',
    //     createdAt: '22-Februari-2004',
    //   },
    //   {
    //     id: 2,
    //     bookingCode: 'BK-220204',
    //     adminId: 1,
    //     courtId: 1,
    //     customerName: 'Kurniawan',
    //     bookingDate: '22-02-04',
    //     startTime: '14:00',
    //     endTime: '18:00',
    //     totalHours: 4,
    //     baseAmount: 4000000,
    //     totalAmount: 3500000,
    //     status: 'ACTIVE',
    //     createdAt: '22-Februari-2004',
    //   },
    // ];

    // this.activeBookings = dummyValue;

    this.bookings.bookings().subscribe({
      next: (response) => {
        console.log(response);
        this.activeBookings = response;
      },
    });
  }
}

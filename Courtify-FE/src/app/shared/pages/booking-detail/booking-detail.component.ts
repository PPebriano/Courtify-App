import { BookingsService } from './../../../core/services/bookings.service';
import { Component, inject, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BookingDetailResponseType } from '../../../core/models/response/booking-detail-response-type';
import { APP_ROUTES } from '../../../shared/constants/routes';
import {
  DateIndoPipe,
  TimeFormatPipe,
} from '../../../core/formatter/date-formatter-pipe';

@Component({
  selector: 'app-booking-detail',
  standalone: true,
  imports: [DateIndoPipe, TimeFormatPipe],
  templateUrl: './booking-detail.component.html',
  styleUrl: './booking-detail.component.css',
})
export class BookingDetailComponent {
  private route = inject(ActivatedRoute);
  private bookingsService = inject(BookingsService);
  private router = inject(Router);

  @Input({ required: true }) isReadOnly = false;

  bookingDetail: BookingDetailResponseType | null = null;
  isSubmitting = false;

  ngOnInit() {
    const bookingId = this.route.snapshot.paramMap.get('bookId');
    this.fetchBookingDetail(bookingId);
  }

  private fetchBookingDetail(bookingId: string | null) {
    if (bookingId) {
      this.bookingsService.bookingDetail(Number(bookingId)).subscribe({
        next: (response) => {
          console.log(response);
          this.bookingDetail = response;
        },
      });
    }
  }

  completeBooking() {
    const booking = this.bookingDetail;
    if (booking) {
      this.bookingsService.bookingStatus(booking.id, 'DONE').subscribe({
        next: (response) => {
          console.log(response.status);
          this.router.navigate([APP_ROUTES.HISTORY], { replaceUrl: true });
        },
      });
    }
  }

  cancelBooking() {
    const booking = this.bookingDetail;
    if (booking) {
      this.bookingsService.cancelBooking(booking.id).subscribe({
        next: () => {
          this.router.navigate([APP_ROUTES.BOOK], { replaceUrl: true });
        },
      });
    }
  }
}

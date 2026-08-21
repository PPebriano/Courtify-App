import { BookingsService } from './../../../core/services/bookings.service';
import { Component, inject, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BookingDetailResponseType } from '../../../core/models/response/booking-detail-response-type';

@Component({
  selector: 'app-booking-detail',
  standalone: true,
  imports: [],
  templateUrl: './booking-detail.component.html',
  styleUrl: './booking-detail.component.css',
})
export class BookingDetailComponent {
  private route = inject(ActivatedRoute);
  private bookingsService = inject(BookingsService);

  @Input({ required: true }) isReadOnly = false;

  bookingDetail: BookingDetailResponseType | null = null;
  isSubmitting = false;

  ngOnInit() {
    const bookingId = this.route.snapshot.paramMap.get('bookingId');
    this.fetchBookingDetail(bookingId);
  }

  private fetchBookingDetail(bookingId: string | null) {
    // const dummyDataActive = {
    //   id: 1,
    //   bookingCode: 'BK-20260822-001',
    //   adminId: 101,
    //   courtId: 12,
    //   customerName: 'Budi Santoso',
    //   bookingDate: '2026-08-22',
    //   startTime: '09:00',
    //   endTime: '13:00',
    //   totalHours: 4,
    //   baseAmount: 550000,
    //   grandTotal: 590000,
    //   status: 'ACTIVE',
    //   createdAt: '20-10-2026',
    //   bookingAddOns: [
    //     {
    //       id: 10,
    //       bookingId: 1,
    //       equipmentAddOnsId: 101,
    //       quantity: 2,
    //       unitPrice: 20000,
    //       subtotal: 40000,
    //       equipmentName: 'Raket Padel',
    //     },
    //     {
    //       id: 10,
    //       bookingId: 1,
    //       equipmentAddOnsId: 101,
    //       quantity: 2,
    //       unitPrice: 20000,
    //       subtotal: 40000,
    //       equipmentName: 'Raket Padel',
    //     },
    //   ],
    // };

    // const dummyDataCancelled = {
    //   id: 2,
    //   bookingCode: 'BK-20260822-002',
    //   adminId: 101,
    //   courtId: 15,
    //   customerName: 'Dadang Gunawan',
    //   bookingDate: '2026-08-22',
    //   startTime: '14:00',
    //   endTime: '18:00',
    //   totalHours: 4,
    //   baseAmount: 600000,
    //   grandTotal: 690000,
    //   status: 'DONE',
    //   createdAt: '2026-08-21T08:30:00Z',
    //   bookingAddOns: [
    //     {
    //       id: 11,
    //       bookingId: 2,
    //       equipmentAddOnsId: 101,
    //       quantity: 2,
    //       unitPrice: 25000,
    //       subtotal: 50000,
    //       equipmentName: 'Raket Badminton Yonex',
    //     },
    //     {
    //       id: 12,
    //       bookingId: 2,
    //       equipmentAddOnsId: 102,
    //       quantity: 1,
    //       unitPrice: 40000,
    //       subtotal: 40000,
    //       equipmentName: 'Kok Badminton (1 Slop)',
    //     },
    //   ],
    // };

    // this.bookingDetail = dummyDataCancelled;
    // this.bookingDetail = dummyDataActive;

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
        },
      });
    }
  }

  cancelBooking() {}
}

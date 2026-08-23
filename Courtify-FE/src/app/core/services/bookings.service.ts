import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BookingRequestType } from '../models/request/booking-request-type';
import { environtment } from '../environments/environment';
import { BookingResponseType } from '../models/response/booking-response-type';
import { BookingDetailResponseType } from '../models/response/booking-detail-response-type';
import { BookingStatusResponseType } from '../models/response/booking-status-response-type';

@Injectable({
  providedIn: 'root',
})
export class BookingsService {
  http = inject(HttpClient);

  booking(payload: BookingRequestType) {
    console.log('Booking payload:', payload);
    return this.http.post<BookingRequestType>(
      `${environtment.apiUrl}/api/bookings`,
      payload,
    );
  }

  bookings(status: string) {
    return this.http.get<BookingResponseType[]>(
      `${environtment.apiUrl}/api/bookings`,
      { params: { status } },
    );
  }

  bookingDetail(id: number) {
    return this.http.get<BookingDetailResponseType>(
      `${environtment.apiUrl}/api/bookings/${id}`,
    );
  }

  bookingStatus(id: number, status: string) {
    return this.http.put<BookingStatusResponseType>(
      `${environtment.apiUrl}/api/bookings/${id}/status`,
      { status },
    );
  }

  cancelBooking(id: number) {
    return this.http.delete(`${environtment.apiUrl}/api/bookings/${id}`);
  }
}

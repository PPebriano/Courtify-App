import { Component, Input } from '@angular/core';
import { BookingResponseType } from '../../../core/models/response/booking-response-type';
import { APP_ROUTES } from '../../constants/routes';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-booking-card',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './booking-card.component.html',
  styleUrl: './booking-card.component.css',
})
export class BookingCardComponent {
  @Input({ required: true }) route!: string;
  @Input({ required: true }) booking!: BookingResponseType;
  @Input({ required: true }) illustrationImage?: string;
}

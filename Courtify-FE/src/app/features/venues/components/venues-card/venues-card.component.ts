import { Component, EventEmitter, Input } from '@angular/core';
import { VenueResponseType } from '../../../../core/models/response/venue-response-type';
import { RouterLink } from '@angular/router';
import { APP_ROUTES } from '../../../../shared/constants/routes';
import { CourtResponseType } from '../../../../core/models/response/court-response-type';

@Component({
  selector: 'app-venues-card',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './venues-card.component.html',
  styleUrl: './venues-card.component.css',
})
export class VenuesCardComponent {
  readonly APP_ROUTES = APP_ROUTES;

  @Input({ required: true }) venue!: VenueResponseType;
  @Input({ required: true }) illustrationImage?: string;
}

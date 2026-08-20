import { Component, Input } from '@angular/core';
import { VenueDetailResponseType } from '../../../../core/models/response/venue-detail-response-type';

@Component({
  selector: 'app-venue-detail-banner',
  standalone: true,
  imports: [],
  templateUrl: './venue-detail-banner.component.html',
  styleUrl: './venue-detail-banner.component.css',
})
export class VenueDetailBannerComponent {
  @Input({ required: true }) venueDetail?: VenueDetailResponseType | null;
}

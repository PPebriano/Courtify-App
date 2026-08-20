import { CommonModule } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { VenueDetailResponseType } from '../../../../core/models/response/venue-detail-response-type';
import { VenueDetailBannerComponent } from '../../components/venue-detail-banner/venue-detail-banner.component';
import { CourtCardComponent } from '../../components/court-card/court-card.component';
import { VenuesService } from '../../../../core/services/venues.service';

@Component({
  selector: 'app-venue-detail',
  standalone: true,
  imports: [CommonModule, VenueDetailBannerComponent, CourtCardComponent],
  templateUrl: './venue-detail.component.html',
  styleUrl: './venue-detail.component.css',
})
export class VenueDetailComponent {
  private route = inject(ActivatedRoute);
  private venuesService = inject(VenuesService);

  venueDetail = signal<VenueDetailResponseType | null>(null);

  ngOnInit() {
    const venueId = this.route.snapshot.paramMap.get('venueId');
    this.fetchVenueDetail(venueId);
  }

  private fetchVenueDetail(venueId: string | null) {
    console.log('Venue Detail Initiated');
    if (venueId) {
      this.venuesService.venueDetail(Number(venueId)).subscribe({
        next: (response) => {
          console.log(response);
          this.venueDetail.set(response);
        },
      });
    }
  }
}

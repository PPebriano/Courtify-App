import { CommonModule } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { VenueDetailResponseType } from '../../../../core/models/response/venue-detail-response-type';
import { VenueDetailBannerComponent } from '../../components/venue-detail-banner/venue-detail-banner.component';
import { CourtCardComponent } from '../../components/court-card/court-card.component';
import { VenuesService } from '../../../../core/services/venues.service';
import { CourtResponseType } from '../../../../core/models/response/court-response-type';
import { BookingDialogComponent } from '../../components/booking-dialog/booking-dialog.component';
import { VenueResponseType } from '../../../../core/models/response/venue-response-type';

@Component({
  selector: 'app-venue-detail',
  standalone: true,
  imports: [
    CommonModule,
    VenueDetailBannerComponent,
    CourtCardComponent,
    BookingDialogComponent,
  ],
  templateUrl: './venue-detail.component.html',
  styleUrl: './venue-detail.component.css',
})
export class VenueDetailComponent {
  private route = inject(ActivatedRoute);
  private venuesService = inject(VenuesService);

  venueDetail: VenueDetailResponseType | null = null;

  selectedCourt: CourtResponseType | null = null;
  modalIsOpen: boolean = false;

  ngOnInit() {
    const venueId = this.route.snapshot.paramMap.get('venueId');
    this.fetchVenueDetail(venueId);
  }

  private fetchVenueDetail(venueId: string | null) {
    const dummyValue = {
      venueId: 1,
      venueName: 'Courtify Pondok Indah',
      address: 'Jl Pondok Indah',
      phoneNumber: '08998372422',
      isActive: true,
      courts: [
        {
          courtId: 12,
          courtName: 'CTFY Padel-1',
          courtCategory: {
            categoryName: 'Lapangan Padel',
            description: 'Lapangan padel indoor dengan lapangan vinyl',
          },
          hourlyRate: 100000,
          isAvailable: true,
        },
      ],
    };

    this.venueDetail = dummyValue;

    if (venueId) {
      this.venuesService.venueDetail(Number(venueId)).subscribe({
        next: (response) => {
          console.log(response);
          this.venueDetail = response;
        },
      });
    }
  }

  handleCourtSelected(court: CourtResponseType | null) {
    this.selectedCourt = court;
    this.handleToggleModals();
  }

  handleToggleModals() {
    this.modalIsOpen = !this.modalIsOpen;
  }
}

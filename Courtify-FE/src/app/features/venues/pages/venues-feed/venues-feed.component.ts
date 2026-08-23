import { Component, inject, signal, Signal } from '@angular/core';
import { BannerComponent } from '../../../../shared/components/banner/banner.component';
import { VenuesCardComponent } from '../../components/venues-card/venues-card.component';
import { VenueResponseType } from '../../../../core/models/response/venue-response-type';
import { VenuesService } from '../../../../core/services/venues.service';

@Component({
  selector: 'app-venues-feed',
  standalone: true,
  imports: [BannerComponent, VenuesCardComponent],
  templateUrl: './venues-feed.component.html',
  styleUrl: './venues-feed.component.css',
})
export class VenuesFeedComponent {
  private venuesService = inject(VenuesService);

  venues: VenueResponseType[] = [];

  ngOnInit() {
    this.fetchVenues();
  }

  private fetchVenues() {
    // const dummyValue = [
    //   {
    //     id: 1,
    //     nameVenue: 'Courtify Pondok Indah',
    //     address: 'Jl Pondok Indah',
    //     phoneNumber: '08998372422',
    //     isActive: true,
    //   },
    // ];
    // this.venues = dummyValue;

    this.venuesService.venues().subscribe({
      next: (response) => {
        console.log(response);
        this.venues = response;
      },
    });
  }
}

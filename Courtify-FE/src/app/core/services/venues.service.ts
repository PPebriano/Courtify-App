import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { VenueResponseType } from '../models/response/venue-response-type';
import { environtment } from '../environments/environment';
import { VenueDetailResponseType } from '../models/response/venue-detail-response-type';

@Injectable({
  providedIn: 'root',
})
export class VenuesService {
  http = inject(HttpClient);

  venues() {
    return this.http.get<VenueResponseType[]>(
      `${environtment.apiUrl}/api/venues`,
    );
  }

  venueDetail(id: number) {
    return this.http.get<VenueDetailResponseType>(
      `${environtment.apiUrl}/api/venues/${id}`,
    );
  }
}

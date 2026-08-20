import { CourtResponseType } from './court-response-type';

export type VenueDetailResponseType = {
  venueId: number;
  venueName: string;
  address: string;
  phoneNumber: string;
  isActive: false;
  courts: CourtResponseType[];
};

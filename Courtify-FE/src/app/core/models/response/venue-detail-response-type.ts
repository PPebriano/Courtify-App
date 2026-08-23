import { CourtResponseType } from './court-response-type';

export type VenueDetailResponseType = {
  id: number;
  venueName: string;
  address: string;
  phoneNumber: string;
  isActive: boolean;
  courts: CourtResponseType[];
};

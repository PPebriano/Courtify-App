import { CourtCategoryResponseType } from './court-category-response-type';

export type CourtResponseType = {
  courtId: number;
  courtName: string;
  courtCategory: CourtCategoryResponseType;
  hourlyRate: number;
  isAvailable: boolean;
};

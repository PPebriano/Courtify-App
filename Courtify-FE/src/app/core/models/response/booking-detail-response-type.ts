import { AddOnsDetailResponseType } from './add-ons-detail-response-type';

export type BookingDetailResponseType = {
  id: number;
  bookingCode: string;
  adminId: number;
  courtId: number;
  customerName: string;
  bookingDate: string;
  startTime: string;
  endTime: string;
  totalHours: number;
  baseAmount: number;
  grandTotal: number;
  status: string;
  createdAt: string;
  bookingAddOns: AddOnsDetailResponseType[];
};

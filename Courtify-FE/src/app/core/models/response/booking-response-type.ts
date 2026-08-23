export type BookingResponseType = {
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
  totalAmount: number;
  status: string;
  createdAt: string;
};

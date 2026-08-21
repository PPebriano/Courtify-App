export type BookingResponseType = {
  id: number;
  bookingCode: string;
  adminId: number;
  courtId: number;
  customerName: string;
  bookingDate: string;
  startTime: string;
  endTime: string;
  total_hours: number;
  base_amount: number;
  grandTotal: number;
  status: string;
  createdAt: string;
};

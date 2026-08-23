import { EquipmentAddOnsRequestType } from './equipment-add-ons-request-type';

export type BookingRequestType = {
  adminId: number;
  courtId: number;
  customerName: string;
  bookingDate: string;
  startTime: string;
  endTime: string;
  addons: EquipmentAddOnsRequestType[];
};

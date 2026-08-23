import {
  Component,
  EventEmitter,
  inject,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import {
  FormBuilder,
  ReactiveFormsModule,
  Validators,
  FormArray,
} from '@angular/forms';
import { CourtResponseType } from '../../../../core/models/response/court-response-type';
import {
  bookingDateValidator,
  bookingTimeRangeValidator,
  customerNameValidator,
} from '../../../../core/validator/validator';
import { EquipmentAddOnsResponseType } from '../../../../core/models/response/equipment-add-ons-response-type';
import { AddOnsService } from '../../../../core/services/add-ons.service';
import { BookingsService } from '../../../../core/services/bookings.service';
import { AuthService } from '../../../../core/services/auth.service';
import { BookingRequestType } from '../../../../core/models/request/booking-request-type';
import { EquipmentAddOnsRequestType } from '../../../../core/models/request/equipment-add-ons-request-type';

@Component({
  selector: 'app-booking-dialog',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './booking-dialog.component.html',
  styleUrl: './booking-dialog.component.css',
})
export class BookingDialogComponent implements OnInit {
  private formBuilder = inject(FormBuilder);
  private addOnsService = inject(AddOnsService);
  private bookingsService = inject(BookingsService);
  private authService = inject(AuthService);

  @Input({ required: true }) court!: CourtResponseType;
  @Output() close = new EventEmitter<void>();

  equipmentAddOns: EquipmentAddOnsResponseType[] = [];
  isSubmitting = false;
  successMessage: string | null = null;
  errorMessage: string | null = null;

  bookForm = this.formBuilder.group(
    {
      customerName: ['', [Validators.required, customerNameValidator]],
      bookingDate: ['', [Validators.required, bookingDateValidator]],
      startTime: ['', [Validators.required]],
      endTime: ['', [Validators.required]],
      addons: this.formBuilder.array([]),
    },
    { validators: [bookingTimeRangeValidator] },
  );

  ngOnInit() {
    this.fetchEquipmentAddOns();
  }

  get addonsFormArray(): FormArray {
    return this.bookForm.get('addons') as FormArray;
  }

  onSubmit() {
    if (this.bookForm.invalid) {
      this.bookForm.markAllAsTouched();
      return;
    }

    this.resetMessages();
    this.isSubmitting = true;

    const payload = this.buildBookingPayload();
    this.sendBookingRequest(payload);
  }

  private fetchEquipmentAddOns() {
    this.addOnsService.addOns().subscribe({
      next: (response) => {
        this.equipmentAddOns = response;
        this.initAddonsFormArray(response);
      },
    });
  }

  private initAddonsFormArray(data: EquipmentAddOnsResponseType[]) {
    this.addonsFormArray.clear();
    data.forEach((addon) => {
      this.addonsFormArray.push(
        this.formBuilder.group({
          equipmentAddOnsId: [addon.id],
          selected: [false],
          quantity: [1, [Validators.min(1)]],
        }),
      );
    });
  }

  private buildBookingPayload(): BookingRequestType {
    const val = this.bookForm.value;
    const adminIdFromAuth = Number(this.authService.getUserId()) || 0;

    return {
      adminId: adminIdFromAuth,
      courtId: this.court.courtId,
      customerName: val.customerName ?? '',
      bookingDate: val.bookingDate ?? '',
      startTime: val.startTime ?? '',
      endTime: val.endTime ?? '',
      addons: this.getSelectedAddons(),
    };
  }

  private getSelectedAddons(): EquipmentAddOnsRequestType[] {
    const addons = this.bookForm.value.addons || [];
    return addons
      .filter((item: any) => item.selected)
      .map((item: any) => ({
        equipmentAddOnsId: item.equipmentAddOnsId,
        quantity: item.quantity,
      }));
  }

  private sendBookingRequest(payload: BookingRequestType) {
    this.bookingsService.booking(payload).subscribe({
      next: () => this.handleSuccess(),
      error: (err) => this.handleError(err),
    });
  }

  private handleSuccess() {
    this.isSubmitting = false;
    this.successMessage = 'Booking berhasil dibuat!';
    setTimeout(() => this.close.emit(), 1500);
  }

  private handleError(err: any) {
    this.isSubmitting = false;
    this.errorMessage =
      err.error?.message || 'Gagal membuat booking. Silakan coba lagi.';
  }

  private resetMessages() {
    this.errorMessage = null;
    this.successMessage = null;
  }
}

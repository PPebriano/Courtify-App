import {
  Component,
  EventEmitter,
  inject,
  Input,
  input,
  Output,
} from '@angular/core';
import {
  FormBuilder,
  FormsModule,
  ɵInternalFormsSharedModule,
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
@Component({
  selector: 'app-booking-dialog',
  standalone: true,
  imports: [ɵInternalFormsSharedModule, ReactiveFormsModule],
  templateUrl: './booking-dialog.component.html',
  styleUrl: './booking-dialog.component.css',
})
export class BookingDialogComponent {
  private formBuilder = inject(FormBuilder);

  @Input({ required: true }) court!: CourtResponseType;
  @Output() close = new EventEmitter<void>();

  bookForm = this.formBuilder.group(
    {
      customerName: ['', [Validators.required, customerNameValidator]],
      bookingDate: ['', [Validators.required, bookingDateValidator]],
      startTime: ['', [Validators.required]],
      endTime: ['', [Validators.required]],
      addons: this.formBuilder.array([]),
    },
    { Validators: [bookingTimeRangeValidator] },
  );

  availableAddons = [
    { id: 1, name: 'Sewa Raket Padel', price: 25000 },
    { id: 2, name: 'Sepatu Olahraga', price: 30000 },
    { id: 3, name: 'Bola Padel (1 Slop)', price: 15000 },
  ];

  onSubmit() {}

  get addonsFormArray(): FormArray {
    return this.bookForm.get('addons') as FormArray;
  }
}

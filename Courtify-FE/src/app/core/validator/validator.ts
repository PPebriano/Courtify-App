import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function usernameValidator(
  control: AbstractControl,
): ValidationErrors | null {
  const value = control.value as string;
  const isValidUsername = /^[A-Z][a-zA-Z0-9_-]*$/.test(value);

  if (!isValidUsername) {
    return {
      usernameValidator: {
        result: true,
        message: 'Huruf pertama pada username wajib menggunakan huruf kapital',
      },
    };
  }

  return null;
}

export function passwordValidator(
  control: AbstractControl,
): ValidationErrors | null {
  const value = control.value as string;
  const isMinLength = value.length >= 8;
  const startsWithCapital = /^[A-Z]/.test(value);
  const hasSpecialChar = /[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]/.test(value);

  const isValid = isMinLength && startsWithCapital && hasSpecialChar;

  if (!isValid) {
    return {
      passwordValidator: {
        result: true,
        message: 'Password diawali huruf kapital dan memiliki karakter spesial',
      },
    };
  }
  return null;
}

export function customerNameValidator(
  control: AbstractControl,
): ValidationErrors | null {
  const value = control.value as string;
  const isValid = value.length >= 3;

  if (!isValid) {
    return {
      customerNameValidator: {
        result: true,
        message: 'Nama customer minimal tiga huruf',
      },
    };
  }

  return null;
}

export function bookingDateValidator(
  control: AbstractControl,
): ValidationErrors | null {
  const value = control.value as string;

  const selectedDate = new Date(value);
  const today = new Date();
  today.setHours(0, 0, 0, 0);

  if (selectedDate < today) {
    return {
      bookingDateValidator: {
        result: true,
        message: 'Tanggal booking tidak boleh di masa lalu',
      },
    };
  }
  return null;
}
export function bookingTimeRangeValidator(
  control: AbstractControl,
): ValidationErrors | null {
  const startTime = control.get('startTime')?.value as string;
  const endTime = control.get('endTime')?.value as string;

  if (!startTime || !endTime) return null;

  return checkOperationalHours(startTime, endTime);
}

function checkOperationalHours(
  startTime: string,
  endTime: string,
): ValidationErrors | null {
  const OPERATIONAL_START = '08:00';
  const OPERATIONAL_END = '22:00';

  if (startTime < OPERATIONAL_START || startTime > OPERATIONAL_END) {
    return {
      timeOperationalError: {
        message: 'Jam mulai harus di antara jam 08:00 sampai 22:00',
      },
    };
  }

  if (endTime < OPERATIONAL_START || endTime > OPERATIONAL_END) {
    return {
      timeOperationalError: {
        message: 'Jam selesai harus di antara jam 08:00 sampai 22:00',
      },
    };
  }

  if (startTime >= endTime) {
    return {
      timeSequenceError: {
        message: 'Jam selesai harus setelah jam mulai',
      },
    };
  }

  return null;
}

import { AbstractControl, ValidationErrors } from '@angular/forms';

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

import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'formatDateIndo',
  standalone: true,
})
export class DateIndoPipe implements PipeTransform {
  transform(value: string | Date | undefined | null): string {
    if (!value) return '';

    const date = new Date(value);
    if (isNaN(date.getTime())) return '';

    const day = String(date.getDate()).padStart(2, '0');
    const year = date.getFullYear();

    const monthsIndo = [
      'Januari',
      'Februari',
      'Maret',
      'April',
      'Mei',
      'Juni',
      'Juli',
      'Agustus',
      'September',
      'Oktober',
      'November',
      'Desember',
    ];

    const monthName = monthsIndo[date.getMonth()];
    return `${day} ${monthName} ${year}`;
  }
}

@Pipe({
  name: 'formatTime',
  standalone: true,
})
export class TimeFormatPipe implements PipeTransform {
  transform(value: string | undefined | null): string {
    if (!value) return '';
    // Memotong string HH:mm:ss menjadi HH:mm
    const parts = value.split(':');
    if (parts.length >= 2) {
      return `${parts[0]}:${parts[1]}`;
    }
    return value;
  }
}

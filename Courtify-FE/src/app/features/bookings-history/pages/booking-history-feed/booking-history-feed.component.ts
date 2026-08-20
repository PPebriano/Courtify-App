import { Component } from '@angular/core';
import { BannerComponent } from '../../../../shared/components/banner/banner.component';

@Component({
  selector: 'app-booking-history-feed',
  standalone: true,
  imports: [BannerComponent],
  templateUrl: './booking-history-feed.component.html',
  styleUrl: './booking-history-feed.component.css',
})
export class BookingHistoryFeedComponent {}

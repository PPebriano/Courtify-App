import { Component } from '@angular/core';
import { BannerComponent } from '../../../../shared/components/banner/banner.component';

@Component({
  selector: 'app-booking-feed',
  standalone: true,
  imports: [BannerComponent],
  templateUrl: './booking-feed.component.html',
  styleUrl: './booking-feed.component.css',
})
export class BookingFeedComponent {}

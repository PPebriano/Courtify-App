import { Routes } from '@angular/router';
import { APP_ROUTES } from './shared/constants/routes';
import { authGuard } from './core/guard/auth.guard';

export const routes: Routes = [
  {
    path: APP_ROUTES.LOGIN,
    loadComponent: () =>
      import('./features/auth/login/login.component').then(
        (m) => m.LoginComponent,
      ),
  },
  {
    path: APP_ROUTES.VENUES,
    canActivate: [authGuard],
    children: [
      {
        path: '',
        loadComponent: () =>
          import('./features/venues/pages/venues-feed/venues-feed.component').then(
            (m) => m.VenuesFeedComponent,
          ),
      },
      {
        path: ':venueId',
        loadComponent: () =>
          import('./features/venues/pages/venue-detail/venue-detail.component').then(
            (m) => m.VenueDetailComponent,
          ),
      },
    ],
  },
  {
    path: APP_ROUTES.BOOK,
    canActivate: [authGuard],
    loadComponent: () =>
      import('./features/booking/pages/booking-feed/booking-feed.component').then(
        (m) => m.BookingFeedComponent,
      ),
  },
  {
    path: APP_ROUTES.HISTORY,
    canActivate: [authGuard],
    loadComponent: () =>
      import('./features/bookings-history/pages/booking-history-feed/booking-history-feed.component').then(
        (m) => m.BookingHistoryFeedComponent,
      ),
  },
];

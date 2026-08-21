import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CourtResponseType } from '../../../../core/models/response/court-response-type';

@Component({
  selector: 'app-court-card',
  standalone: true,
  imports: [],
  templateUrl: './court-card.component.html',
  styleUrl: './court-card.component.css',
})
export class CourtCardComponent {
  @Input({ required: true }) court!: CourtResponseType;
  @Input({ required: true }) illustrationImage?: string;
  @Output() selectedCourt = new EventEmitter<CourtResponseType>();

  onBookClicked() {
    this.selectedCourt.emit(this.court);
  }
}

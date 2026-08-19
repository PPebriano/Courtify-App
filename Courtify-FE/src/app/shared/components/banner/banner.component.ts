import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, input, Output } from '@angular/core';

@Component({
  selector: 'app-banner',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './banner.component.html',
  styleUrl: './banner.component.css',
})
export class BannerComponent {
  @Input({ required: true }) title: string = '';
  @Input() subtitle?: string;

  @Input() actionButtonLabel?: string;

  @Output() actionClicked = new EventEmitter<void>();

  onButtonClick(): void {
    this.actionClicked.emit();
  }
}

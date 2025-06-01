import { Component, input } from '@angular/core';

@Component({
  selector: 'app-media-card',
  imports: [],
  templateUrl: './media-card.component.html',
  styleUrl: './media-card.component.css'
})
export class MediaCardComponent {
  @Input()media!: Media
}

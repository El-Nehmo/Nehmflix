import { Component, Input } from '@angular/core';
import { Media } from '../../models/media.model';


@Component({
  selector: 'app-media-card',
  imports: [],
  templateUrl: './media-card.component.html',
  styleUrl: './media-card.component.css'
})
export class MediaCardComponent {
  @Input() media!: Media
}

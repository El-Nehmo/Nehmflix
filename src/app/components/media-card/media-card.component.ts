import { Component, Input } from '@angular/core';
import { Media } from '../../models/media.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-media-card',
  templateUrl: './media-card.component.html',
  styleUrls: ['./media-card.component.css']
})
export class MediaCardComponent {
  @Input() media!: Media;

  constructor(private router: Router) {}

  voirDetails() {
  this.router.navigate(['/media', this.media.id_tmdb, this.media.type]);
}

}

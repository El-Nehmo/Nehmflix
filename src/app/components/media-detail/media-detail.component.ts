import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TmdbService } from '../../services/tmdb.service';
import { Media } from '../../models/media.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-media-detail',
  imports: [CommonModule],
  templateUrl: './media-detail.component.html',
  styleUrl: './media-detail.component.css'
})
export class MediaDetailComponent implements OnInit{

  media: Media | null = null;

  constructor (
    private route: ActivatedRoute,
    private tmdbService: TmdbService
  ) {}

  ngOnInit(): void {
      const id = this.route.snapshot.paramMap.get('id');
      const type = this.route.snapshot.paramMap.get('type');

      if (id && type) {
        this.tmdbService.getDetails(id, type as 'movie' | 'tv').subscribe((data: Media ) => {
          this.media = data;
          console.log("Média récupére", this.media);
          
        });
      }
  }

}

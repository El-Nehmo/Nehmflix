import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { TmdbService } from '../../services/tmdb.service';
import { Media } from '../../models/media.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-media-detail',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './media-detail.component.html',
  styleUrl: './media-detail.component.css'
})
export class MediaDetailComponent implements OnInit {

  media: Media | null = null;
  genres: string[] = [];
  actors: string[] = [];
  directors: string[] = [];

  constructor(
    private route: ActivatedRoute,
    private tmdbService: TmdbService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    const type = this.route.snapshot.paramMap.get('type');

    if (id && type) {
      this.tmdbService.getDetails(id, type as 'movie' | 'tv').subscribe((data: Media) => {
        this.media = data;
        console.log("affichage du média pour deboggage", data)
        //this.genres = data.genres?.map((g: any) => g.name) || [];
        this.genres = Array.isArray(data.genres) ? data.genres.map((g: any) => g.name) : [];

      });

      this.tmdbService.getCredits(id, type as 'movie' | 'tv').subscribe((credits: any) => {
        this.actors = credits.cast?.slice(0, 5).map((a: any) => a.name) || [];
        this.directors = credits.crew
          ?.filter((c: any) => c.job === 'Director')
          .map((d: any) => d.name) || [];
      });
    }
  }
}



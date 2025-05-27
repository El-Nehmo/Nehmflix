import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Media } from '../../models/media.model';
import { TmdbService } from '../../services/tmdb.service';

@Component({
  selector: 'app-search',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent {
  searchQuery: string = '';
  resultats: Media[] = [];
  aRecherche: boolean = false;

  constructor(private tmdbService: TmdbService) {}

  rechercher() {
    this.aRecherche = true;

    this.tmdbService.search(this.searchQuery).subscribe((data) => {
      this.resultats = data.results.map((media: any) => ({
        id_tmdb: media.id,
        titre: media.title || media.name,
        type: media.media_type,
        annee_sortie: media.release_date?.slice(0, 4) || media.first_air_date?.slice(0, 4),
        note: media.vote_average,
        affiche_url: media.poster_path
          ? 'https://image.tmdb.org/t/p/w500' + media.poster_path
          : '',
        resume: media.overview
      }));
    });
  }
}


import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Media } from '../../models/media.model';


@Component({
  selector: 'app-watchlist',
  standalone: true,
  imports: [RouterModule, CommonModule],
  templateUrl: './watchlist.component.html',
  styleUrl: './watchlist.component.css'
})
export class WatchlistComponent {

  watchlist: Media[] = [
    {
      id_tmdb: 27205,
      titre: 'Inception',
      type: 'movie',
      annee_sortie: '2010',
      note: 8.8,
      affiche_url: 'https://image.tmdb.org/t/p/w500/qmDpIHrmpJINaRKAfWQfftjCdyi.jpg',
      resume: 'Un voleur expérimenté spécialisé dans l\'extraction de secrets du subconscient.',
      genres: [{ id: 28, name: 'Action' }],
      dernier_update: '2025-06-10'
    },
    {
      id_tmdb: 1396,
      titre: 'Breaking Bad',
      type: 'tv',
      annee_sortie: '2008',
      note: 9.5,
      affiche_url: 'https://image.tmdb.org/t/p/w500/eSzpy96DwBujGFj0xMbXBcGcfxX.jpg',
      resume: 'Un professeur de chimie devient fabricant de méthamphétamine.',
      genres: [{ id: 80, name: 'Crime' }],
      dernier_update: '2025-06-10'
    }
  ];

  removeFromWatchlist(id_tmdb: number) {
    this.watchlist = this.watchlist.filter(media => media.id_tmdb !== id_tmdb);
  }

}

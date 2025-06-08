import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environement } from '../..//environments/environment';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Media } from '../models/media.model';

@Injectable({
  providedIn: 'root'
})
export class TmdbService {

  private apiUrl = 'https://api.themoviedb.org/3';
  private apiKey = environement.tmdbApikey

  constructor(private http: HttpClient) { }

  //Fonction de rechcerche film/sémrie
  search(query: string): Observable<any> {
    const url = `${this.apiUrl}/search/multi?api_key=${this.apiKey}&language=fr-FR&query=${encodeURIComponent(query)}`;
    return this.http.get(url);
  }

  
  // Retourne les détails d’un film ou d’une série
  getDetails(id: string, type: 'movie' | 'tv'): Observable<Media> {
  const url = `${this.apiUrl}/${type}/${id}?api_key=${this.apiKey}&language=fr-FR`;
  return this.http.get<any>(url).pipe(
    map(data => ({
      id_tmdb: data.id,
      titre: data.title || data.name,
      type: type,
      annee_sortie: (data.release_date || data.first_air_date || '').slice(0, 4),
      note: data.vote_average,
      affiche_url: `https://image.tmdb.org/t/p/w500${data.poster_path}`,
      genres: data.genres,
      resume: data.overview
    }))
  );
}
  // Méthode pour récupérer les acteurs, réalisateurs, etcr
  getCredits(id: string, type: 'movie' | 'tv'): Observable<any> {
    const url = `${this.apiUrl}/${type}/${id}/credits?api_key=${this.apiKey}&language=fr-FR`;
    return this.http.get(url);
}



}

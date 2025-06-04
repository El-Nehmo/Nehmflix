import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environement } from '../..//environments/environment';
import { Observable } from 'rxjs';

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
  getDetails(id: string, type: 'movie' | 'tv'): Observable<any> {
    const url = `${this.apiUrl}/${type}/${id}?api_key=${this.apiKey}&language=fr-FR`;
    return this.http.get(url);
  }


}

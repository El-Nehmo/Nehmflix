export interface Media {
  id_tmdb: number;
  titre: string;
  type: 'movie' | 'tv';
  annee_sortie: string;
  note: number;
  affiche_url: string;
  resume: string;
  genres?: { id: number; name: string }[];
  dernier_update?: string;
}

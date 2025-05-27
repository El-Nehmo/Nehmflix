export interface Media {
  id_tmdb: number;
  titre: string;
  type: 'film' | 'serie';
  annee_sortie: string;
  note: number;
  affiche_url: string;
  resume: string;
  dernier_update?: string;
}

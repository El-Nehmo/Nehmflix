using MySql.Data.MySqlClient;
using NehmFlix.Domain.Entities;
using NehmFlix.Domain.Enums;

namespace NehmFlix.Application.Services;

/*
 Service de gestion des watchlists utilisateur.
 Permet d'ajouter, supprimer et récupérer les médias d'un utilisateur.
 Utilise des requêtes SQL directes via MySql.Data.
*/
public class WatchlistService
{
    private readonly string _connectionString;

    /*
     Constructeur qui reçoit la chaîne de connexion MySQL.
     Nécessaire pour se connecter à la base.
    */
    public WatchlistService(string connectionString)
    {
        _connectionString = connectionString;
    }

    /*
     Ajoute un média dans la watchlist d’un utilisateur.
     Le type peut être 'regarde' ou 'a_regarder'.
    */
    public void AjouterMedia(int utilisateurId, int mediaId, ListeType typeListe)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var query = @"INSERT INTO users_media (utilisateur_id, media_id, type_liste)
                      VALUES (@UtilisateurId, @MediaId, @TypeListe);";

        using var cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@UtilisateurId", utilisateurId);
        cmd.Parameters.AddWithValue("@MediaId", mediaId);
        cmd.Parameters.AddWithValue("@TypeListe", typeListe.ToString().ToLower());

        cmd.ExecuteNonQuery();
    }

    /*
     Supprime un média de la watchlist d’un utilisateur.
     */
    public void SupprimerMedia(int utilisateurId, int mediaId)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var query = @"DELETE FROM users_media
                      WHERE utilisateur_id = @UtilisateurId AND media_id = @MediaId;";

        using var cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@UtilisateurId", utilisateurId);
        cmd.Parameters.AddWithValue("@MediaId", mediaId);

        cmd.ExecuteNonQuery();
    }

    /*
     Récupère tous les médias d’un utilisateur selon le type ('regarde' ou 'a_regarder').
     Retourne une liste d’objets Media.
    */
    public List<Media> RecupererWatchlist(int utilisateurId, ListeType typeListe)
    {
        var liste = new List<Media>();

        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var query = @"
            SELECT m.* FROM media m
            INNER JOIN users_media um ON m.id_tmdb = um.media_id
            WHERE um.utilisateur_id = @UtilisateurId AND um.type_liste = @TypeListe;";

        using var cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@UtilisateurId", utilisateurId);
        cmd.Parameters.AddWithValue("@TypeListe", typeListe.ToString().ToLower());

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            liste.Add(new Media
            {
                IdTmdb = Convert.ToInt32(reader["id_tmdb"]),
                Titre = reader["titre"].ToString()!,
                Type = Enum.Parse<MediaType>(reader["type"].ToString()!, true),
                AnneeSortie = reader["annee_sortie"]?.ToString(),
                Note = reader["note"] as decimal?,
                AfficheUrl = reader["affiche_url"]?.ToString(),
                Resume = reader["resume"]?.ToString(),
                DernierUpdate = Convert.ToDateTime(reader["dernier_update"]),
                Genres = reader["genres"]?.ToString()
            });
        }

        return liste;
    }
}

using MySql.Data.MySqlClient;
using NehmFlix.Domain.Entities;

namespace NehmFlix.Application.Services;

// Ce service permet d'ajouter et de récupérer des films/séries (médias) depuis la base de données.
public class MediaService
{
    private readonly string _connectionString;

    
    public MediaService(string connectionString)
    {
        _connectionString = connectionString;
    }

    // Ajoute un film ou une série dans la table `media`
    public void AddMedia(Media media)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var query = @"
            INSERT INTO media (id_tmdb, titre, type, annee_sortie, note, affiche_url, resume, genres)
            VALUES (@IdTmdb, @Titre, @Type, @AnneeSortie, @Note, @AfficheUrl, @Resume, @Genres);";

        using var cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@IdTmdb", media.IdTmdb);
        cmd.Parameters.AddWithValue("@Titre", media.Titre);
        cmd.Parameters.AddWithValue("@Type", media.Type.ToString().ToLower()); // "film" ou "serie"
        cmd.Parameters.AddWithValue("@AnneeSortie", media.AnneeSortie);
        cmd.Parameters.AddWithValue("@Note", media.Note);
        cmd.Parameters.AddWithValue("@AfficheUrl", media.AfficheUrl);
        cmd.Parameters.AddWithValue("@Resume", media.Resume);
        cmd.Parameters.AddWithValue("@Genres", media.Genres);

        cmd.ExecuteNonQuery();
    }

    // Récupère tous les médias de la base (films et séries)
    public List<Media> GetAllMedia()
    {
        var medias = new List<Media>();

        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var query = "SELECT * FROM media;";
        using var cmd = new MySqlCommand(query, connection);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            var media = new Media
            {
                IdTmdb = Convert.ToInt32(reader["id_tmdb"]),
                Titre = reader["titre"].ToString()!,
                Type = Enum.Parse<Domain.Enums.MediaType>(reader["type"].ToString()!, true),
                AnneeSortie = reader["annee_sortie"].ToString(),
                Note = reader["note"] as decimal?,
                AfficheUrl = reader["affiche_url"].ToString(),
                Resume = reader["resume"].ToString(),
                DernierUpdate = Convert.ToDateTime(reader["dernier_update"]),
                Genres = reader["genres"].ToString()
            };

            medias.Add(media);
        }

        return medias;
    }
}

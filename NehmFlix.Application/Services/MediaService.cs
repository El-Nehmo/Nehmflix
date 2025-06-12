using MySql.Data.MySqlClient;
using NehmFlix.Domain.Entities;

/*
 Service de gestion des médias (films/séries).
 Utilisé pour insérer un média s’il n’existe pas encore,
 ou récupérer un média par son ID (id_tmdb).
*/
namespace NehmFlix.Application.Services
{
    public class MediaService
    {
        private readonly string _connectionString;

        /*
         Constructeur qui prend la chaîne de connexion SQL.
        */
        public MediaService(string connectionString)
        {
            _connectionString = connectionString;
        }

        /*
         Ajoute un média à la base s’il n’existe pas déjà.
         Cela évite les doublons si plusieurs utilisateurs ajoutent le même film.
        */
        public void AddMediaIfNotExists(Media media)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var checkQuery = "SELECT COUNT(*) FROM media WHERE id_tmdb = @IdTmdb;";
            using var checkCmd = new MySqlCommand(checkQuery, connection);
            checkCmd.Parameters.AddWithValue("@IdTmdb", media.IdTmdb);

            var exists = Convert.ToInt32(checkCmd.ExecuteScalar()) > 0;
            if (exists) return;

            var insertQuery = @"INSERT INTO media (id_tmdb, titre, type, annee_sortie, note, affiche_url, resume, dernier_update, genres)
                                VALUES (@IdTmdb, @Titre, @Type, @AnneeSortie, @Note, @AfficheUrl, @Resume, @DernierUpdate, @Genres);";

            using var insertCmd = new MySqlCommand(insertQuery, connection);
            insertCmd.Parameters.AddWithValue("@IdTmdb", media.IdTmdb);
            insertCmd.Parameters.AddWithValue("@Titre", media.Titre);
            insertCmd.Parameters.AddWithValue("@Type", media.Type.ToString().ToLower());
            insertCmd.Parameters.AddWithValue("@AnneeSortie", media.AnneeSortie ?? (object)DBNull.Value);
            insertCmd.Parameters.AddWithValue("@Note", media.Note ?? (object)DBNull.Value);
            insertCmd.Parameters.AddWithValue("@AfficheUrl", media.AfficheUrl ?? (object)DBNull.Value);
            insertCmd.Parameters.AddWithValue("@Resume", media.Resume ?? (object)DBNull.Value);
            insertCmd.Parameters.AddWithValue("@DernierUpdate", media.DernierUpdate);
            insertCmd.Parameters.AddWithValue("@Genres", media.Genres ?? (object)DBNull.Value);

            insertCmd.ExecuteNonQuery();
        }

        /*
         Récupère un média à partir de son ID TMDb.
         Renvoie null si aucun média trouvé.
        */
        public Media? GetMediaById(int idTmdb)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var query = "SELECT * FROM media WHERE id_tmdb = @IdTmdb;";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@IdTmdb", idTmdb);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Media
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
            }

            return null;
        }
    }
}


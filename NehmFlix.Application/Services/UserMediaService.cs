using MySql.Data.MySqlClient;
using NehmFlix.Domain.Entities;
using NehmFlix.Domain.Enums;

namespace NehmFlix.Application.Services;

public class UserMediaService
{
    private readonly string _connectionString;

    public UserMediaService(string connectionString)
    {
        _connectionString = connectionString;
    }

    /*
     * Ajoute un média à la liste d’un utilisateur.
     * La table `users_media` enregistre ce lien.
     */
    public void AddToList(int userId, int mediaId, ListeType typeListe)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var query = @"
            INSERT INTO users_media (utilisateur_id, media_id, type_liste)
            VALUES (@UserId, @MediaId, @TypeListe);";

        using var cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@UserId", userId);
        cmd.Parameters.AddWithValue("@MediaId", mediaId);
        cmd.Parameters.AddWithValue("@TypeListe", typeListe.ToString().ToLower());

        cmd.ExecuteNonQuery();
    }

    /*
     * Supprime un média de la liste d’un utilisateur.
     */
    public void RemoveFromList(int userId, int mediaId)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var query = @"
            DELETE FROM users_media 
            WHERE utilisateur_id = @UserId AND media_id = @MediaId;";

        using var cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@UserId", userId);
        cmd.Parameters.AddWithValue("@MediaId", mediaId);

        cmd.ExecuteNonQuery();
    }

    /*
     * Récupère la liste de tous les médias liés à un utilisateur.
     */
    public List<int> GetUserMediaIds(int userId)
    {
        var mediaIds = new List<int>();

        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var query = @"
            SELECT media_id FROM users_media
            WHERE utilisateur_id = @UserId;";

        using var cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@UserId", userId);

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            mediaIds.Add(Convert.ToInt32(reader["media_id"]));
        }

        return mediaIds;
    }
}


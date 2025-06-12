using MySql.Data.MySqlClient;
using NehmFlix.Domain.Entities;

namespace NehmFlix.Infrastructure.Services;

/*
 Service de gestion des utilisateurs.
 Fournit des méthodes pour s'inscrire et se connecter.
*/
public class UserService
{
    private readonly string _connectionString;

    /*
     Constructeur qui reçoit la chaîne de connexion
     à la base de données MySQL.
    */
    public UserService(string connectionString)
    {
        _connectionString = connectionString;
    }

    /*
     Enregistre un nouvel utilisateur dans la base.
     On insère son nom, email et mot de passe.
    */
    public void Register(User user)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var query = @"INSERT INTO users (nom, email, mot_de_passe)
                      VALUES (@Nom, @Email, @MotDePasse);";

        using var cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Nom", user.Nom);
        cmd.Parameters.AddWithValue("@Email", user.Email);
        cmd.Parameters.AddWithValue("@MotDePasse", user.MotDePasse);

        cmd.ExecuteNonQuery();
    }

    /*
     Vérifie les identifiants de connexion (email + mot de passe).
     Si c’est correct, retourne l’utilisateur trouvé.
     Sinon, retourne null.
    */
    public User? Login(string email, string motDePasse)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var query = @"SELECT * FROM users WHERE email = @Email AND mot_de_passe = @MotDePasse;";

        using var cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Email", email);
        cmd.Parameters.AddWithValue("@MotDePasse", motDePasse);

        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new User
            {
                Id = Convert.ToInt32(reader["id"]),
                Nom = reader["nom"].ToString()!,
                Email = reader["email"].ToString()!,
                MotDePasse = reader["mot_de_passe"].ToString()!,
                DateInscription = Convert.ToDateTime(reader["date_inscription"])
            };
        }

        return null;
    }
}



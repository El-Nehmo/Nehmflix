using MySql.Data.MySqlClient;
using NehmFlix.Domain.Entities;
using BCrypt.Net;
using NehmFlix.Application.Services;

/*
 Service responsable de la gestion des utilisateurs :
 - Enregistrement avec hachage sécurisé du mot de passe
 - Connexion avec vérification du hash
 Ce service se connecte directement à MySQL via une chaîne de connexion.
*/
namespace NehmFlix.Infrastructure.Services;

public class UserService
{
    private readonly string _connectionString;

    /*
     Initialise le service avec la chaîne de connexion MySQL
     fournie au moment de l’instanciation.
    */
    public UserService(string connectionString)
    {
        _connectionString = connectionString;
    }

    /*
     Inscrit un nouvel utilisateur dans la base de données :
     - Le mot de passe est haché avec BCrypt avant stockage
     - Empêche le stockage en clair
     - Ajoute nom, email et mot de passe haché
    */
    
    /*
    Gère l'enregistrement d'un nouvel utilisateur.
    - Vérifie que les champs ne sont pas vides (nom, email, mot de passe)
    - Le mot de passe est hashé avec BCrypt avant stockage
    - Empêche le stockage en clair
    - Ajoute nom, email et mot de passe hashé dans la base MySQL
    */
    public void Register(User user)
    {
        // Vérifie les données utilisateur avant de continuer
        if (!UserValidator.EstValide(user))
        {
            throw new ArgumentException("Les données utilisateur sont invalides.");
        }

        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var query = @"INSERT INTO users (nom, email, mot_de_passe)
                  VALUES (@Nom, @Email, @MotDePasse);";

        using var cmd = new MySqlCommand(query, connection);

        // Hash du mot de passe avec BCrypt
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.MotDePasse);

        cmd.Parameters.AddWithValue("@Nom", user.Nom);
        cmd.Parameters.AddWithValue("@Email", user.Email);
        cmd.Parameters.AddWithValue("@MotDePasse", hashedPassword);

        cmd.ExecuteNonQuery();
    }


    /*
     Vérifie les identifiants pour une tentative de connexion :
     - Récupère l’utilisateur via son email
     - Compare le mot de passe fourni avec le hash enregistré
     - Si ok → retourne l’utilisateur ; sinon → null
    */
    public User? Login(string email, string motDePasse)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var query = @"SELECT * FROM users WHERE email = @Email;";

        using var cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Email", email);

        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            var hashedPassword = reader["mot_de_passe"].ToString()!;
            var isPasswordValid = BCrypt.Net.BCrypt.Verify(motDePasse, hashedPassword);

            if (isPasswordValid)
            {
                return new User
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Nom = reader["nom"].ToString()!,
                    Email = reader["email"].ToString()!,
                    MotDePasse = hashedPassword,
                    DateInscription = Convert.ToDateTime(reader["date_inscription"])
                };
            }
        }

        return null;
    }
}



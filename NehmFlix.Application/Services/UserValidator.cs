using System.Text.RegularExpressions;
using NehmFlix.Domain.Entities;

namespace NehmFlix.Application.Services;

/*
 Classe de validation des données utilisateur :
 - Vérifie que les champs requis sont bien remplis
 - S’assure que l’email est valide
 - Vérifie que le mot de passe est suffisamment long
*/
public static class UserValidator
{
    /*
     Vérifie les règles de validation d’un utilisateur :
     - Nom non vide
     - Email bien formé
     - Mot de passe ≥ 6 caractères
     Retourne true si les données sont valides, sinon false
    */
    public static bool EstValide(User user)
    {
        if (string.IsNullOrWhiteSpace(user.Nom))
            return false;

        if (string.IsNullOrWhiteSpace(user.Email) || !EmailEstValide(user.Email))
            return false;

        if (string.IsNullOrWhiteSpace(user.MotDePasse) || user.MotDePasse.Length < 6)
            return false;

        return true;
    }

    /*
     Vérifie que l’email correspond à un format classique :
     exemple : exemple@email.com
    */
    private static bool EmailEstValide(string email)
    {
        return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }
}

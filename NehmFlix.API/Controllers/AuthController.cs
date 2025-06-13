using Microsoft.AspNetCore.Mvc;
using NehmFlix.Application.Services;
using NehmFlix.Domain.Entities;

namespace NehmFlix.API.Controllers;

/*
 Contrôleur dédié à l’authentification :
 - Inscription d’un utilisateur
 - Connexion via email/mot de passe

 Utilise UserService pour toute la logique métier.
*/
[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly UserService _userService;

    // Le service UserService est injecté pour traiter les opérations liées aux utilisateurs.
    public AuthController(UserService userService)
    {
        _userService = userService;
    }

    /*
     Inscrit un nouvel utilisateur avec les données reçues en JSON (nom, email, mot de passe).
     On appelle la méthode Register du UserService, qui peut renvoyer une exception si les données
     ne sont pas valides.
     On gère les erreurs spécifiques et les erreurs serveur classiques.
    */
    [HttpPost("register")]
    public IActionResult Register([FromBody] User user)
    {
        try
        {
            _userService.Register(user);
            return Ok(new { message = "Utilisateur inscrit avec succès." });
        }
        catch (ArgumentException ex)
        {
            // Mauvaise saisie ou utilisateur déjà existant
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception)
        {
            // Problème plus sérieux (ex : base de données inaccessible)
            return StatusCode(500, new { message = "Une erreur est survenue côté serveur." });
        }
    }

    /*
     Vérifie les identifiants (email + mot de passe).
     Si valides, renvoie les infos de l’utilisateur.
     Sinon, retourne une erreur 401.
    */
    [HttpPost("login")]
    public IActionResult Login([FromBody] User loginData)
    {
        var user = _userService.Login(loginData.Email, loginData.MotDePasse);
        if (user != null)
        {
            return Ok(user);
        }
        return Unauthorized(new { message = "Identifiants invalides." });
    }
}


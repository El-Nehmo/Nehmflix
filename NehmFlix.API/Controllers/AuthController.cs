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
     Si tout se passe bien, renvoie un message de succès.
    */
    [HttpPost("register")]
    public IActionResult Register([FromBody] User user)
    {
        try
        {
            _userService.Register(user);
            return Ok(new { message = "Utilisateur inscrit avec succès." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Erreur : {ex.Message}" });
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

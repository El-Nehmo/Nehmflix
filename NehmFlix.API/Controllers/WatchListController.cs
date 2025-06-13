using Microsoft.AspNetCore.Mvc;
using NehmFlix.Application.Services;
using NehmFlix.Domain.Entities;
using NehmFlix.Domain.Enums;

namespace NehmFlix.API.Controllers;

/*
 Contrôleur dédié à la gestion de la watchlist de l’utilisateur :
 - Récupération des médias
 - Ajout et suppression de médias

 Utilise WatchListService pour toute la logique métier liée à la watchlist.
*/
[ApiController]
[Route("watchlist")]
public class WatchListController : ControllerBase
{
    private readonly WatchListService _watchListService;

    // Le service WatchListService est injecté pour manipuler les watchlists utilisateur.
    public WatchListController(WatchListService watchListService)
    {
        _watchListService = watchListService;
    }

    /*
     Récupère tous les médias présents dans la watchlist d’un utilisateur.
     On peut filtrer par type de liste : "regarde" ou "a_regarder".
    */
    [HttpGet("{userId}")]
    public IActionResult RecupererWatchlist(int userId, [FromQuery] string typeListe)
    {
        var liste = _watchListService.RecupererWatchlist(userId, Enum.Parse<ListeType>(typeListe, true));
        return Ok(liste);
    }

    /*
     Ajoute un média dans la watchlist de l’utilisateur.
     Le corps de la requête doit contenir un UserMedia avec l’ID utilisateur,
     l’ID TMDB du média et le type de liste ("regarde", "a_regarder").
    */
    [HttpPost]
    public IActionResult AjouterMedia([FromBody] UserMedia userMedia)
    {
        try
        {
            _watchListService.AjouterMedia(userMedia.UtilisateurId, userMedia.MediaId, userMedia.TypeListe);
            return Ok(new { message = "Média ajouté à la watchlist." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /*
     Supprime un média de la watchlist d’un utilisateur.
     On doit fournir l’ID utilisateur et l’ID TMDB du média à supprimer.
    */
    [HttpDelete("{userId}/{mediaId}")]
    public IActionResult SupprimerMedia(int userId, int mediaId)
    {
        try
        {
            _watchListService.SupprimerMedia(userId, mediaId);
            return Ok(new { message = "Média retiré de la watchlist." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}


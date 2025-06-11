namespace NehmFlix.Application.DTOs;

public class WatchlistItemDto
{
    public int IdTmdb { get; set; }
    public string Titre { get; set; } = null!;
    public string Type { get; set; } = null!;
    public string? AnneeSortie { get; set; }
    public decimal? Note { get; set; }
    public string? AfficheUrl { get; set; }
    public string? Resume { get; set; }
    public string? Genres { get; set; }
    public string TypeListe { get; set; } = null!;
}

namespace NehmFlix.Application.DTOs;

public class MediaDto
{
    public int IdTmdb { get; set; }
    public string Titre { get; set; } = null!;
    public string? AfficheUrl { get; set; }
    public decimal? Note { get; set; }
    public string? Resume { get; set; }
    public string? Genres { get; set; }
}

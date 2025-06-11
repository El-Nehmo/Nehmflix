using NehmFlix.Domain.Enums;

namespace NehmFlix.Domain.Entities;

public class Media
{
    public int IdTmdb { get; set; }
    public string Titre { get; set; } = null!;
    public MediaType Type { get; set; }
    public string? AnneeSortie { get; set; }
    public decimal? Note { get; set; }
    public string? AfficheUrl { get; set; }
    public string? Resume { get; set; }
    public DateTime DernierUpdate { get; set; } = DateTime.UtcNow;
    public string? Genres { get; set; }

    public ICollection<UserMedia> Users { get; set; } = new List<UserMedia>();
}

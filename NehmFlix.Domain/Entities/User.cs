namespace NehmFlix.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Nom { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string MotDePasse { get; set; } = null!;
    public DateTime DateInscription { get; set; } = DateTime.UtcNow;

    public ICollection<UserMedia> Watchlist { get; set; } = new List<UserMedia>();
}

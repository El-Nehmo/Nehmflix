using NehmFlix.Domain.Enums;

namespace NehmFlix.Domain.Entities;

public class UserMedia
{
    public int Id { get; set; }

    public int UtilisateurId { get; set; }
    public User Utilisateur { get; set; } = null!;

    public int MediaId { get; set; }
    public Media Media { get; set; } = null!;

    public ListeType TypeListe { get; set; }
    public DateTime DateAjout { get; set; } = DateTime.UtcNow;
}

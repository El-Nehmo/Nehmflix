using NehmFlix.Domain.Enums;

namespace NehmFlix.Application.DTOs;

public class UserMediaDto
{
    public int Id { get; set; }
    public MediaDto Media { get; set; } = null!;
    public ListeType TypeListe { get; set; }
    public DateTime DateAjout { get; set; }
}

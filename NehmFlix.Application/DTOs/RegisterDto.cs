namespace NehmFlix.Application.DTOs;

public class RegisterDto
{
    public string Nom { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string MotDePasse { get; set; } = null!;
}

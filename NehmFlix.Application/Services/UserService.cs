using NehmFlix.Application.DTOs;
using NehmFlix.Domain.Entities;

namespace NehmFlix.Application.Services;

public class UserService
{
    public UserDto ToDto(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Nom = user.Nom,
            Email = user.Email
        };
    }
}

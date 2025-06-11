using NehmFlix.Application.DTOs;
using NehmFlix.Domain.Entities;

namespace NehmFlix.Application.Services;

public class UserMediaService
{
    private readonly MediaService _mediaService = new();

    public UserMediaDto ToDto(UserMedia userMedia)
    {
        return new UserMediaDto
        {
            Id = userMedia.Id,
            Media = _mediaService.ToDto(userMedia.Media),
            TypeListe = userMedia.TypeListe,
            DateAjout = userMedia.DateAjout
        };
    }
}

using NehmFlix.Application.DTOs;
using NehmFlix.Domain.Entities;

namespace NehmFlix.Application.Services;

public class MediaService
{
    public MediaDto ToDto(Media media)
    {
        return new MediaDto
        {
            IdTmdb = media.IdTmdb,
            Titre = media.Titre,
            AfficheUrl = media.AfficheUrl,
            Note = media.Note,
            Resume = media.Resume,
            Genres = media.Genres
        };
    }
}

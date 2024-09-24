using AutoMapper;
using mangas.Domain.Entities;
using Mangas.Domain.Datos;

namespace Mangas.Services.MappingsM;

public class ResponseMappingProfile:Profile
{
    public ResponseMappingProfile()
    {
        CreateMap<Manga, MangaDto>()
        .ForMember(
            dest=> dest.PublicationYear,
            opt => opt.MapFrom(src=> src.PublicationDate)
        );
    }
}
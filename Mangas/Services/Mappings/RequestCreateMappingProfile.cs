using AutoMapper;
using mangas.Domain.Entities;
using Mangas.Domain.Datos;
namespace Mangas.Services.MappingsM;

public class RequestCreateMappingProfile:Profile
{
    public RequestCreateMappingProfile()
    {
        CreateMap<MangaDto, Manga>()
        .AfterMap 
        (
            (src, dest) =>
            {
                dest.PublicationDate = DateTime.Now;
            }
        );
    }
}
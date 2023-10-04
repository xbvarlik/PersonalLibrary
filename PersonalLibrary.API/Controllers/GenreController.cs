using PersonalLibrary.API.DTOs.GenreDTOs;
using PersonalLibrary.API.Mappings;
using PersonalLibrary.API.Services;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.API.Controllers;

public class GenreController : BaseController<Genre, GenreCreateDto, GenreReadDto, GenreUpdateDto, GenreQueryFilterDto>
{
    public GenreController(GenreService service, GenreMapper mapper) : base(service, mapper)
    {
    }
}
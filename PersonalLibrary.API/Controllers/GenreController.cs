using PersonalLibrary.API.Mappings;
using PersonalLibrary.API.Services;
using PersonalLibrary.Core.DTOs.GenreDTOs;
using PersonalLibrary.Core.Entities;

namespace PersonalLibrary.API.Controllers;

public class GenreController : BaseController<Genre, GenreCreateDto, GenreReadDto, GenreUpdateDto, GenreQueryFilterDto>
{
    public GenreController(GenreService service, GenreMapper mapper) : base(service, mapper)
    {
    }
}
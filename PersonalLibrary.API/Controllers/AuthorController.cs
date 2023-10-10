using PersonalLibrary.API.Mappings;
using PersonalLibrary.API.Services;
using PersonalLibrary.Core.DTOs.AuthorDTOs;
using PersonalLibrary.Core.Entities;

namespace PersonalLibrary.API.Controllers;

public class AuthorController : BaseController<Author, AuthorCreateDto, AuthorReadDto, AuthorUpdateDto, AuthorQueryFilterDto>
{
    public AuthorController(AuthorService service, AuthorMapper mapper) : base(service, mapper)
    {
    }
}
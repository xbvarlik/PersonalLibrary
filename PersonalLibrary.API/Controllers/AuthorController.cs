using PersonalLibrary.API.DTOs.AuthorDTOs;
using PersonalLibrary.API.Mappings;
using PersonalLibrary.API.Services;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.API.Controllers;

public class AuthorController : BaseController<Author, AuthorCreateDto, AuthorReadDto, AuthorUpdateDto, AuthorQueryFilterDto>
{
    public AuthorController(AuthorService service, AuthorMapper mapper) : base(service, mapper)
    {
    }
}
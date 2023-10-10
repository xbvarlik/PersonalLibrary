using PersonalLibrary.API.Core.BooksOfUserDTOs;
using PersonalLibrary.API.Mappings;
using PersonalLibrary.API.Services;
using PersonalLibrary.Core.DTOs.BooksOfUserDTOs;
using PersonalLibrary.Core.Entities;

namespace PersonalLibrary.API.Controllers;

public class BooksOfUserController : BaseController<BooksOfUser, BooksOfUserCreateDto, BooksOfUserReadDto, BooksOfUserUpdateDto, BooksOfUserQueryFilterDto>
{
    public BooksOfUserController(BooksOfUserService service, BooksOfUserMapper mapper) : base(service, mapper)
    {
    }
}

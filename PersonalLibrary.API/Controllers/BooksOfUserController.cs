using PersonalLibrary.API.DTOs.BooksOfUserDTOs;
using PersonalLibrary.API.Mappings;
using PersonalLibrary.API.Services;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.API.Controllers;

public class BooksOfUserController : BaseController<BooksOfUser, BooksOfUserCreateDto, BooksOfUserReadDto, BooksOfUserUpdateDto, BooksOfUserQueryFilterDto>
{
    public BooksOfUserController(BooksOfUserService service, BooksOfUserMapper mapper) : base(service, mapper)
    {
    }
}

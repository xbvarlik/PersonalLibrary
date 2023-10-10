using PersonalLibrary.API.Mappings;
using PersonalLibrary.API.Services;
using PersonalLibrary.Core.DTOs.TagsOfUserDTOs;
using PersonalLibrary.Core.Entities;

namespace PersonalLibrary.API.Controllers;

public class TagsOfUserController : BaseController<TagsOfUser, TagsOfUserCreateDto, TagsOfUserReadDto, TagsOfUserUpdateDto, TagsOfUserQueryFilterDto>
{
    public TagsOfUserController(TagsOfUserService service, TagsOfUserMapper mapper) : base(service, mapper)
    {
    }
}
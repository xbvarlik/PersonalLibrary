using PersonalLibrary.API.DTOs.PublisherDTOs;
using PersonalLibrary.API.Mappings;
using PersonalLibrary.API.Services;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.API.Controllers;

public class PublisherController : BaseController<Publisher, PublisherCreateDto, PublisherReadDto, PublisherUpdateDto, PublisherQueryFilterDto>
{
    public PublisherController(PublisherService service, PublisherMapper mapper) : base(service, mapper)
    {
    }
}
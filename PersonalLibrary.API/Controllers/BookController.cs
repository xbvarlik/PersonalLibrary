using Microsoft.AspNetCore.Mvc;
using PersonalLibrary.API.Mappings;
using PersonalLibrary.API.Services;
using PersonalLibrary.Core.DTOs.BookDTOs;
using PersonalLibrary.Core.DTOs.CommunicationDTOs;
using PersonalLibrary.Core.Entities;
using PersonalLibrary.Utilities.Managers;

namespace PersonalLibrary.API.Controllers;

public class BookController : BaseController<Book, BookCreateDto, BookReadDto, BookUpdateDto, BookQueryFilterDto>
{
    public BookController(BookService service, BookMapper mapper) : base(service, mapper)
    {
    }

    public override async Task<IActionResult> GetAllAsync(BookQueryFilterDto query)
    {
        var entities = await Service.GetAllAsync(query);
        if (entities.Count == 0) return NoContent();

        var dtos = entities.Select(x =>
        {
            var dto = Mapper.ToDto(x, true);
            
            var image = ImageManager.ReadImageFromLocalStorage(x.CoverImage, Constants.Constants.DefaultImagePath);
            if(image != null) dto.CoverImage = image;

            return dto;
        }).ToList();
        
        var response = ResponseDto<List<BookReadDto>>.Success(200, dtos);
        return CreateActionResult(response);
    }

    public override async Task<IActionResult> GetByIdAsync(int id)
    {
        var entity = await Service.GetByIdAsync(id);
        var dto = Mapper.ToDto(entity, true);
        
        var image = ImageManager.ReadImageFromLocalStorage(entity.CoverImage, Constants.Constants.DefaultImagePath);
        
        if(image != null) dto.CoverImage = image;
        
        var response = ResponseDto<BookReadDto>.Success(200, dto);
        return CreateActionResult(response);
    }
}

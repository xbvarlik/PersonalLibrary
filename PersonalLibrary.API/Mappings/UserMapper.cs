using Microsoft.Extensions.FileProviders.Physical;
using PersonalLibrary.API.DTOs.UserDTOs;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.API.Mappings;

public class UserMapper 
{
    public  User ToEntity(UserCreateDto dto)
    {
        return new User
        {
            UserName = dto.FirstName + dto.LastName,
            Email = dto.Email,
        };
    }

    public  User ToEntity(UserUpdateDto dto, User entity)
    {
        if (dto.FirstName != null || dto.LastName != null) entity.UserName = (dto.FirstName + dto.LastName);
        entity.Email = dto.Email ?? entity.Email;

        return entity;
    }

    public  UserReadDto ToDto(User entity)
    {
        return new UserReadDto
        {
            UserId = entity.Id,
            UserName = entity.UserName,
            Email = entity.Email,
            Books = entity.BooksOfUsers?.Select(b => new BooksOfUserMapper().ToDto(b)).ToList()
        };
    }
}

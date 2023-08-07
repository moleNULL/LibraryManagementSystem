using AutoMapper;
using LibraryManagementSystem.BLL.Models.Dtos.BookDtos;
using LibraryManagementSystem.BLL.Models.Entities.BookEntities;

namespace LibraryManagementSystem.PL.Mapping;

public class BookDescriptionResolver : IMemberValueResolver<BookDto, BookEntity, string?, object?>
{
    public object? Resolve(BookDto source, BookEntity destination, 
        string? sourceMember, object? destMember, ResolutionContext context)
    {
        if (source.Description is not null)
        {
            return new DescriptionEntity()
            {
                BookId = source.Id,
                Description = source.Description
            };    
        }

        return null;
    }
}
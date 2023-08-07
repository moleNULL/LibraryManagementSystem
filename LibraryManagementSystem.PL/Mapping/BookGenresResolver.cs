using AutoMapper;
using LibraryManagementSystem.BLL.Models.Dtos.BookDtos;
using LibraryManagementSystem.BLL.Models.Entities.BookEntities;

namespace LibraryManagementSystem.PL.Mapping;

public class BookGenresResolver : IMemberValueResolver<BookDto, BookEntity, IEnumerable<int>, object>
{
    public object Resolve(BookDto source, BookEntity destination, 
        IEnumerable<int> sourceMember, object destMember, ResolutionContext context)
    {
        return source.GenreIds.Select(i => new BookGenreEntity
        {
            BookId = destination.Id,
            GenreId = i,
        }).ToList();
    }
}
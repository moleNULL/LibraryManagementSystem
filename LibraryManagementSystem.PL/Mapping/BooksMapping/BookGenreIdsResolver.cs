using AutoMapper;
using LibraryManagementSystem.BLL.Models.Dtos.BookDtos;
using LibraryManagementSystem.BLL.Models.Entities.BookEntities;

namespace LibraryManagementSystem.PL.Mapping.BooksMapping
{
    public class BookGenreIdsResolver: IMemberValueResolver<BookEntity, BookDto, ICollection<BookGenreEntity>, object>
    {
        public object Resolve(BookEntity source, BookDto destination, 
            ICollection<BookGenreEntity> sourceMember, object destMember, ResolutionContext context)
        {
            return source.BookGenres.Select(bg => bg.GenreId);
        }
    }
}
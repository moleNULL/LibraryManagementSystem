using AutoMapper;
using LibraryManagementSystem.BLL.Models.Dtos;
using LibraryManagementSystem.BLL.Models.Entities.BookEntities;
using LibraryManagementSystem.PL.Configurations;
using Microsoft.Extensions.Options;

namespace LibraryManagementSystem.PL.Mapping
{
    public class BookPictureResolver : IMemberValueResolver<BookEntity, BookDto, string?, object>
    {
        private readonly BookConfig _bookConfig;

        public BookPictureResolver(IOptionsSnapshot<BookConfig> config)
        {
            _bookConfig = config.Value;
        }

        public object Resolve(BookEntity source, BookDto destination, string? sourceMember, 
            object destMember, ResolutionContext context)
        {
            return $"{_bookConfig.ImgUrl}/{sourceMember}";
        }
    }
}

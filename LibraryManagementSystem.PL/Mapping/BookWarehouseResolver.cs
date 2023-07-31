using AutoMapper;
using LibraryManagementSystem.BLL.Models.Dtos;
using LibraryManagementSystem.BLL.Models.Entities.BookEntities;

namespace LibraryManagementSystem.PL.Mapping;

public class BookWarehouseResolver : IMemberValueResolver<BookDto, BookEntity, WarehouseDto, object>
{
    public object Resolve(BookDto source, BookEntity destination, WarehouseDto sourceMember, object destMember,
        ResolutionContext context)
    {
        return new WarehouseEntity
        {
            Price = sourceMember.Price,
            Quantity = sourceMember.Quantity,
            BookId = source.Id
        };
    }
}
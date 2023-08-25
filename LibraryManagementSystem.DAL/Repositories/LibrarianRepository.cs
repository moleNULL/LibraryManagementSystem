using LibraryManagementSystem.BLL.Comparers.LibraryComparers;
using LibraryManagementSystem.BLL.Exceptions;
using LibraryManagementSystem.BLL.Models.Entities;
using LibraryManagementSystem.BLL.Repositories.Interfaces.LibrarianRepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.DAL.Repositories
{
    public class LibrarianRepository : ILibrarianRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public LibrarianRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    
        public async Task<IEnumerable<LibrarianEntity>> GetLibrariansAsync()
        {
            return await _dbContext.Librarians.ToListAsync();
        }

        public async Task<LibrarianEntity?> GetLibrarianByIdAsync(int id)
        {
            return await _dbContext.Librarians.FirstOrDefaultAsync(librarian => librarian.Id == id);
        }

        public async Task<LibrarianEntity?> GetLibrarianByEmailAsync(string email)
        {
            return await _dbContext.Librarians.FirstOrDefaultAsync(librarian => librarian.Email == email);
        }

        public async Task<int> AddLibrarianAsync(LibrarianEntity librarianEntity)
        {
            _dbContext.Librarians.Add(librarianEntity);
            await _dbContext.SaveChangesAsync();

            return librarianEntity.Id;
        }

        public async Task<bool> UpdateLibrarianAsync(LibrarianEntity librarianEntity)
        {
            var existingLibrarianEntity = await _dbContext.Librarians
                .FirstOrDefaultAsync(librarian => librarian.Id == librarianEntity.Id);

            if (existingLibrarianEntity is not null)
            {
                var librarianComparer = new LibraryEqualityEntityComparer();
                if (!librarianComparer.Equals(existingLibrarianEntity, librarianEntity))
                {
                    existingLibrarianEntity.FirstName = librarianEntity.FirstName;
                    existingLibrarianEntity.LastName = librarianEntity.LastName;
                    existingLibrarianEntity.Email = librarianEntity.Email;
                    existingLibrarianEntity.PictureName = librarianEntity.PictureName;
                    existingLibrarianEntity.EntryDate = librarianEntity.EntryDate;

                    _dbContext.Librarians.Update(existingLibrarianEntity);
                    int countUpdated = await _dbContext.SaveChangesAsync();

                    return countUpdated > 0;
                }

                return true;
            }

            throw new NotFoundException($"There is no librarian with Id: {librarianEntity.Id}");
        }

        public async Task<bool> DeleteLibrariansAsync(IEnumerable<int> librarianIds)
        {
            bool areAnyDeleted = false;
            foreach (int id in librarianIds)
            {
                bool result = await DeleteLibrarianByIdAsync(id);
                areAnyDeleted |= result; // if any librarian is deleted return true
            }

            return areAnyDeleted;
        }

        public async Task<bool> DeleteLibrarianByIdAsync(int id)
        {
            var librarianToDelete = await _dbContext.Librarians
                .FirstOrDefaultAsync(librarian => librarian.Id == id);
        
            if (librarianToDelete is not null)
            {
                _dbContext.Librarians.Remove(librarianToDelete);
                int countDeleted = await _dbContext.SaveChangesAsync();

                return countDeleted > 0;
            }

            return false;
        }
    }
}
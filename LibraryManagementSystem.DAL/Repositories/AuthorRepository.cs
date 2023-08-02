using LibraryManagementSystem.BLL.Comparers;
using LibraryManagementSystem.BLL.Models.Entities.BookEntities;
using LibraryManagementSystem.BLL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.DAL.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public AuthorRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<AuthorEntity>> GetAuthorsAsync()
    {
        return await _dbContext.Authors.ToListAsync();
    }

    public async Task<AuthorEntity?> GetAuthorByIdAsync(int id)
    {
        return await _dbContext.Authors.FirstOrDefaultAsync(author => author.Id == id);
    }

    public async Task<int> AddAuthorAsync(AuthorEntity authorEntity)
    {
        _dbContext.Authors.Add(authorEntity);
        await _dbContext.SaveChangesAsync();

        return authorEntity.Id;
    }

    public async Task<bool> UpdateAuthorAsync(AuthorEntity authorEntity)
    {
        var existingAuthorEntity = 
            await _dbContext.Authors.FirstOrDefaultAsync(author => author.Id == authorEntity.Id);

        if (existingAuthorEntity is not null)
        {
            var authorComparer = new AuthorEntityComparer();
            if (!authorComparer.Equals(existingAuthorEntity, authorEntity))
            {
                existingAuthorEntity.FirstName = authorEntity.FirstName;
                existingAuthorEntity.LastName = authorEntity.LastName;  
                
                _dbContext.Authors.Update(existingAuthorEntity);
                int countUpdated = await _dbContext.SaveChangesAsync();

                return countUpdated > 0;
            }
        }

        return false;
    }

    public async Task<bool> DeleteAuthorsAsync(IEnumerable<int> authorIds)
    {
        bool areAnyDeleted = false;
        foreach (var id in authorIds)
        {
            bool result = await DeleteAuthorByIdAsync(id);
            areAnyDeleted |= result; // if any author is deleted return true
        }

        return areAnyDeleted;
    }

    public async Task<bool> DeleteAuthorByIdAsync(int id)
    {
        var authorToDelete = await _dbContext.Authors.FirstOrDefaultAsync(author => author.Id == id);
        if (authorToDelete is not null)
        {
            _dbContext.Authors.Remove(authorToDelete);
            int countDeleted = await _dbContext.SaveChangesAsync();

            return countDeleted > 0;
        }

        return false;
    }
}
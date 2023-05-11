using Book_API.Models;

namespace Book_API.Repository.IRepository
{
    public interface IAuthorRepository :IRepository<Author>
    {
        Task<Author> UpdateAsync(Author entity);
    }
}

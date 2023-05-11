using Book_API.Models;

namespace Book_API.Repository.IRepository
{
    public interface IBookRepository: IRepository<Book>
    {
        Task<Book> UpdateAsync(Book entity);
    }
}

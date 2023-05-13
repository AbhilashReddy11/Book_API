using Book_API.Models;
using Book_API.Models.DTO;

namespace Book_API.Repository.IRepository
{
    public interface IBookRepository: IRepository<Book>
    {
        Task<Book> UpdateAsync(Book entity);
    }
}

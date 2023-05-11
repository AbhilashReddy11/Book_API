using Book_API.Models;

namespace Book_API.Repository.IRepository
{
    public interface IPublisherRepository : IRepository<Publisher>
    {
        Task<Publisher> UpdateAsync(Publisher entity);
    }
}

using Book_API.Data;
using Book_API.Models;
using Book_API.Repository.IRepository;

namespace Book_API.Repository
{
    public class PublisherRepository : Repository<Publisher>, IPublisherRepository
    {

        private readonly ApplicationDbContext _db;
        public PublisherRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Publisher> UpdateAsync(Publisher entity)
        {

            _db.Publishers.Update(entity);
            await _db.SaveChangesAsync();
            return entity;

        }
    }
   
}

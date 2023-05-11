using Book_API.Data;
using Book_API.Models;
using Book_API.Repository.IRepository;

namespace Book_API.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {

        private readonly ApplicationDbContext _db;
        public BookRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Book> UpdateAsync(Book entity)
        {

            _db.Books.Update(entity);
            await _db.SaveChangesAsync();
            return entity;

        }
    }
}

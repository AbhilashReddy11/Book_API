using Book_API.Data;
using Book_API.Models;
using Book_API.Repository.IRepository;

namespace Book_API.Repository
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        private readonly ApplicationDbContext _db;
        public AuthorRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Author> UpdateAsync(Author entity)
        {

               _db.Authors.Update(entity);
            await _db.SaveChangesAsync();
            return entity;



        }



    }
}

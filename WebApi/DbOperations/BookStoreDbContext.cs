using Microsoft.EntityFrameworkCore;
using WebApi.Entity;

namespace WebApi.DbOperations
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options) { }
       
        public DbSet<Book> Books { get; set; }

    }
}

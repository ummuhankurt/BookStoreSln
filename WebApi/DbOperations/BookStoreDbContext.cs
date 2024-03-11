using Microsoft.EntityFrameworkCore;
using WebApi.Entity;

namespace WebApi.DbOperations
{
    public class BookStoreDbContext : DbContext,IBookStoreDbContext
    { 
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options) { }
       
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }

        public override int SaveChanges() // Hem IBookStoreDbContext üzerinden erişilebilir, hem de hala dbContext'in işini yapıyor.
        {
            return base.SaveChanges();
        }
    }
}

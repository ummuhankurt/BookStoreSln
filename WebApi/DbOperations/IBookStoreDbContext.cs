using Microsoft.EntityFrameworkCore;
using WebApi.Entity;

namespace WebApi.DbOperations
{
    public interface IBookStoreDbContext
    {
        DbSet<Book> Books { get; set; }
        DbSet<Genre> Genres { get; set; }
        int SaveChanges();
    }
}

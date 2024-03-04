using Microsoft.EntityFrameworkCore;
using WebApi.Entity;

namespace WebApi.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }
                context.Genres.AddRange(
                    new Genre
                    {
                        Name = "Personel Growth"
                    },
                    new Genre
                    {
                        Name = "Science Fiction"
                    },
                    new Genre
                    {
                        Name = "Romance"
                    }

                    );
                context.Books.AddRange
                (

                    new Book
                    {
                        //Id = 1,
                        Title = "Lean Startup",
                        GenreId = 1,
                        PageCount = 200,
                        PublishDate = new DateTime(2001, 06, 12)
                    },
                    new Book
                    {
                        //Id = 2,
                        Title = "Herland",
                        GenreId = 2,
                        PageCount = 250,
                        PublishDate = new DateTime(2010, 05, 23)
                    },
                    new Book
                    {
                        //Id = 3,
                        Title = "Dune",
                        GenreId = 2,
                        PageCount = 540,
                        PublishDate = new DateTime(2001, 12, 21)
                    }
                );

                context.Authors.AddRange
                (
                    new Author
                    {
                        Name = "Eric",
                        Surname = "Reis",
                        DateOfBirth = new DateTime(1978,9,22),
                        BookId = 1,
                    },
                    new Author
                    {
                        Name = "Charlotte",
                        Surname = "Perkins Gilman",
                        DateOfBirth = new DateTime(1860, 8, 17),
                        BookId= 2,
                    },
                    new Author
                    {
                        Name = "Frank",
                        Surname = "Herbert",
                        DateOfBirth= new DateTime(1920,9,8),
                        BookId = 3
                    }
                );

                context.SaveChanges();
            }
        }
    }
}

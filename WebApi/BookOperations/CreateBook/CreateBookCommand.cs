using WebApi.DbOperations;
using WebApi.Entity;

namespace WebApi.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public CreateBookModel Model { get; set; }
        public CreateBookCommand(BookStoreDbContext bookStoreDbContext)
        {
            _dbContext = bookStoreDbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);
            if (book is not null)
                throw new InvalidOperationException("Kitap zaten mevcut");
            book = new Book();
            book.Title = Model.Title;
            book.PublishDate = Model.PublishDate;
            book.GenreId = Model.GenreId;
            book.PageCount = Model.PageCount;
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }
    }

    public class CreateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}

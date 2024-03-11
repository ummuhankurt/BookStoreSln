using WebApi.DbOperations;

namespace WebApi.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommand
    {
        public UpdateBookModel Model { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        public int BookId { get; set; }
        public UpdateBookCommand(IBookStoreDbContext bookStoreDbContext)
        {
            _dbContext = bookStoreDbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(book => book.Id == BookId);
            if (book is null)
                throw new InvalidOperationException("Böyle bir kitap yok.");
            book.Title = Model.Title != default ? Model.Title : book.Title;
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            _dbContext.SaveChanges();

        }
    }

    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }

    }
}

using WebApi.DbOperations;

namespace WebApi.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly IBookStoreDbContext _dbContext;
        public int BookId { get; set; }
        public DeleteBookCommand(IBookStoreDbContext bookStoreDbContext)
        {
            _dbContext = bookStoreDbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(book => book.Id == BookId);
            if (book is null)
                throw new InvalidOperationException("Silinecek bir kitap bulunamadı");
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
        }
    }
}

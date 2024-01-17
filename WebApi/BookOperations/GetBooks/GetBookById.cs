using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBookById
    {
        private readonly BookStoreDbContext _dbContext;
        public GetBookById(BookStoreDbContext bookStoreDbContext)
        {
            _dbContext = bookStoreDbContext;
        }

        public GetByIdViewModel Handle(int id)
        { 
            var book = _dbContext.Books.Where(book => book.Id == id).FirstOrDefault();
            if (book == null)
            {
                throw new InvalidCastException("Kitap bulunamadı");
            }
            GetByIdViewModel wm = new GetByIdViewModel
            {
                Genre = ((GenreEnum)book.GenreId).ToString(),
                PageCount = book.PageCount,
                PublishDate = book.PublishDate,
                Title = book.Title

            };
            return wm;
        }
    }
    public class GetByIdViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}

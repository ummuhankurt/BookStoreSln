using WebApi.Common;
using WebApi.DbOperations;
using WebApi.Entity;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _bookStoreDbContext;
        public GetBooksQuery(BookStoreDbContext bookStoreDbContext)
        {
            _bookStoreDbContext = bookStoreDbContext;
        }

        public List<BooksViewModel> Handle()
        {
            var bookList = _bookStoreDbContext.Books.OrderBy(x => x.Id).ToList();
            List<BooksViewModel> wm = new List<BooksViewModel>();
            foreach (var book in bookList)
            {
                wm.Add(
                    new BooksViewModel
                    {
                        Id = book.Id,
                        Title = book.Title,
                        PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy"),
                        PageCount = book.PageCount,
                        Genre = ((GenreEnum)book.GenreId).ToString(),
                    });
            }

            return wm;
        }
    }
    public class BooksViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }

    }
}

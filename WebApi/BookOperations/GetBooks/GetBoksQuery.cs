using WebApi.Common;
using WebApi.DbOperations;
using WebApi.Entity;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBoksQuery
    {
        private readonly BookStoreDbContext _bookStoreDbContext;
        public GetBoksQuery(BookStoreDbContext bookStoreDbContext)
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
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }

    }
}

using AutoMapper;
using WebApi.Common;
using WebApi.DbOperations;
using WebApi.Entity;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _bookStoreDbContext;
        private readonly IMapper _mapper;
        public GetBooksQuery(BookStoreDbContext bookStoreDbContext,IMapper mapper)
        {
            _bookStoreDbContext = bookStoreDbContext;
            _mapper = mapper;
        }

        public List<BooksViewModel> Handle()
        {
            var bookList = _bookStoreDbContext.Books.OrderBy(x => x.Id).ToList();
            List<BooksViewModel> wm = _mapper.Map<List<BooksViewModel>>(bookList); 
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

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DbOperations;
using WebApi.Entity;

namespace WebApi.Application.BookOperations.Queries.GetBooks
{
    public class GetBooksQuery
    {
        private readonly IBookStoreDbContext _bookStoreDbContext;
        private readonly IMapper _mapper;
        public GetBooksQuery(IBookStoreDbContext bookStoreDbContext, IMapper mapper)
        {
            _bookStoreDbContext = bookStoreDbContext;
            _mapper = mapper;
        }

        public List<BooksViewModel> Handle()
        {
            var bookList = _bookStoreDbContext.Books.Include(x => x.Genre).OrderBy(x => x.Id).ToList();
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

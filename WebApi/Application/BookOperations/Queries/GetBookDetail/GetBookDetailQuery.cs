using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int BookId { get; set; }
        public GetBookDetailQuery(IBookStoreDbContext bookStoreDbContext, IMapper mapper)
        {
            _dbContext = bookStoreDbContext;
            _mapper = mapper;
        }

        public GetByIdViewModel Handle()
        {
            var book = _dbContext.Books.Include(x => x.Genre).Where(book => book.Id == BookId).FirstOrDefault();
            if (book is null)
                throw new InvalidOperationException("Kitap bulunamadı");
            GetByIdViewModel vm = _mapper.Map<GetByIdViewModel>(book);


            return vm;
        }
    }
    public class GetByIdViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}

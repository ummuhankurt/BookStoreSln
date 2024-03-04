using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int AuthorId { get; set; }
        public GetAuthorDetailQuery(BookStoreDbContext dbContext , IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public AuthorDetailViewModel Handle()
        {
            var book = _dbContext.Authors.Include(x => x.Book).Where(x => x.Id == AuthorId).FirstOrDefault();
            if(book is null)
                throw new InvalidOperationException("Yazar bulunamadı");
            AuthorDetailViewModel vm = _mapper.Map<AuthorDetailViewModel>(book);
            return vm;
        }
    }

    public class AuthorDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Book { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}

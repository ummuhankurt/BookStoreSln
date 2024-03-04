using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int AuthorId { get; set; }
        public DeleteAuthorCommand(BookStoreDbContext context , IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if (author is null)
                throw new InvalidOperationException("Silinecek yazar bulunamadı");
            var controlBookId = author.BookId;
            var result = _dbContext.Books.SingleOrDefault(x => x.Id == controlBookId);
            if (result is not null)
                throw new InvalidOperationException("Kitabı yayında olan yazarı silemezsiniz. Yazarı silmek isriyorsanız önce kitabı silmelisiniz");
            _dbContext.Authors.Remove(author);
            _dbContext.SaveChanges();
        }
    }
}

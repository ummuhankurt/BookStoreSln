using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entity;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand
    {
        public CreateAuthorModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateAuthorCommand(BookStoreDbContext context , IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(x => x.Name == Model.Name);
            if(author is not null)
                throw new InvalidOperationException("Yazar zaten mevcut");
            author = _mapper.Map<Author>(Model);
            _dbContext.Authors.Add(author);
            _dbContext.SaveChanges();
        }
    }

    public class CreateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int BookId { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}

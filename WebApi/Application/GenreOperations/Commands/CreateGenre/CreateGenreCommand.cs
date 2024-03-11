using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entity;

namespace WebApi.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    {
        public readonly IBookStoreDbContext _context;
        public readonly IMapper _mapper;
        public CreateGenreModel Model { get; set; }
        public CreateGenreCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x =>  x.Name == Model.Name);
            if(genre is not null)
                throw new InvalidOperationException("Kitap türü zaten mevcut");
            genre = _mapper.Map<Genre>(Model);
            _context.Genres.Add(genre);
            _context.SaveChanges();
        }
    }

    public class CreateGenreModel
    {
        public string Name { get; set; }
    }
}

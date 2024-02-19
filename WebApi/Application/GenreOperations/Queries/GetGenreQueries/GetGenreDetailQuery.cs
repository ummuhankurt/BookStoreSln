using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entity;

namespace WebApi.Application.GenreOperations.Queries.GetGenreQueries
{
    public class GetGenreDetailQuery
    {
        public readonly BookStoreDbContext _context;
        public readonly IMapper _mapper;
        public int GenreId { get; set; }
        public GetGenreDetailQuery(BookStoreDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public GenreDetailViewModel Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.IsActive && x.Id == GenreId);
            if (genre is null)
                throw new InvalidOperationException("Genre bulunamdı");
            GenreDetailViewModel returnObj = _mapper.Map<GenreDetailViewModel>(genre);
            return returnObj;
        }
    }
    public class GenreDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

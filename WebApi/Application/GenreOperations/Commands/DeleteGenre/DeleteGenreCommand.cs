using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        public readonly BookStoreDbContext _context;
        public int GenreId { get; set; }
        public DeleteGenreCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.Id == GenreId);
            if(genre is null)
                throw new InvalidOperationException("Silinecek bir kitap bulunamadı");
            _context.Genres.Remove(genre);
            _context.SaveChanges();
        }
    }
}

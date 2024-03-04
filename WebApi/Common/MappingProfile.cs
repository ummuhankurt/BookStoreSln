using AutoMapper;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Queries.GetGenreQueries;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Entity;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<CreateGenreModel, Genre>();
            CreateMap<CreateAuthorModel, Author>();
            CreateMap<Book, GetByIdViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<Author, AuthorDetailViewModel>().ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Book.Title));
            CreateMap<Genre, GenreDetailViewModel>();
            CreateMap<Book,BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<Author, AuthorsViewModel>().ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Book.Title));
            CreateMap<Genre, GenresViewModel>();
        }
    }
}
 
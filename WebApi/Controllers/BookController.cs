using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DbOperations;
using WebApi.Entity;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;

namespace WebApi.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class BookController : ControllerBase
    {
        // static: uygulama çalıştığı sürece yaşamalı ve uygulama sonlandığında lifecycle'ı sona ermeli
        //private static List<Book> BookList = new List<Book>()
        //{
        //    new Book
        //    {
        //        Id = 1,
        //        Tite = "Lean Startup",
        //        GenreId = 1, // Personel Growth
        //        PageCount = 200,
        //        PublishDate = new DateTime(2001,06,12)
        //    },
        //    new Book
        //    {
        //        Id = 2,
        //        Tite = "Herland",
        //        GenreId = 2, // Science Fiction
        //        PageCount = 250,
        //        PublishDate = new DateTime(2010,05,23)
        //    },
        //    new Book
        //    {
        //        Id = 3,
        //        Tite = "Dune",
        //        GenreId = 2, // Science Fiction
        //        PageCount = 540,
        //        PublishDate = new DateTime(2001,12,21)
        //    }

        //};
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_dbContext, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetByIdViewModel result;
            GetBookDetailQuery getBookById = new GetBookDetailQuery(_dbContext, _mapper);
            getBookById.BookId = id;
            GetBookDetailValidator validator = new GetBookDetailValidator();
            validator.ValidateAndThrow(getBookById);
            result = getBookById.Handle();
            return Ok(result);
        }


        [HttpPost]
        public IActionResult Add([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand createBookCommand = new CreateBookCommand(_dbContext, _mapper);
            createBookCommand.Model = newBook;
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(createBookCommand.Model);
            createBookCommand.Handle();
            return Ok(newBook.Title + " başarılı bir şekilde eklendi");
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateBookModel updatedBook)
        {
            UpdateBookCommand updateBookCommand = new UpdateBookCommand(_dbContext);
            updateBookCommand.Model = updatedBook;
            updateBookCommand.BookId = id;
            UpateBookCommandValidator validator = new UpateBookCommandValidator();
            validator.ValidateAndThrow(updatedBook);
            updateBookCommand.Handle();

            return Ok("Kitap başarılı bir şekilde güncellendi");
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            DeleteBookCommand deleteBookCommand = new DeleteBookCommand(_dbContext);
            deleteBookCommand.BookId = id;
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(deleteBookCommand);
            deleteBookCommand.Handle();

            return Ok("Kitap Silindi.");
        }
    }
}

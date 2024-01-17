using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DbOperations;
using WebApi.Entity;

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
        public BookController(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet] 
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_dbContext);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public GetByIdViewModel GetById(int id)
        {
            try
            {
                GetBookById getBookById = new GetBookById(_dbContext);
                return getBookById.Handle(id);
            }
            catch (Exception)
            {

                throw;
            }
            
        }


        [HttpPost]
        public IActionResult Add([FromBody] CreateBookModel newBook) 
        {
            CreateBookCommand createBookCommand = new CreateBookCommand(_dbContext);
            try
            {
                createBookCommand.Model = newBook;
                createBookCommand.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
            return Ok(newBook.Title + " başarılı bir şekilde eklendi");
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateBookModel updatedBook)
        {
            UpdateBookCommand updateBookCommand = new UpdateBookCommand(_dbContext);
            try
            {
                updateBookCommand.Model = updatedBook;
                updateBookCommand.Handle(id);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            return Ok("Kitap başarılı bir şekilde güncellendi");
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = _dbContext.Books.SingleOrDefault(book => book.Id == id);
            if (book is null)
                return BadRequest("Böyle bir kitap bulunamadı");
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
            return Ok("Kitap Silindi.");
        }
    }
}    

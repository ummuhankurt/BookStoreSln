using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entity;

namespace WebApi.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class BookController : ControllerBase
    {
        // static: uygulama çalıştığı sürece yaşamalı ve uygulama sonlandığında lifecycle'ı sona ermeli
        private static List<Book> BookList = new List<Book>()
        {
            new Book
            {
                Id = 1,
                Tite = "Lean Startup",
                GenreId = 1, // Personel Growth
                PageCount = 200,
                PublishDate = new DateTime(2001,06,12)
            },
            new Book
            {
                Id = 2,
                Tite = "Herland",
                GenreId = 2, // Science Fiction
                PageCount = 250,
                PublishDate = new DateTime(2010,05,23)
            },
            new Book
            {
                Id = 3,
                Tite = "Dune",
                GenreId = 2, // Science Fiction
                PageCount = 540,
                PublishDate = new DateTime(2001,12,21)
            }

        };
        
        [HttpGet]
        public List<Book> GetBooks()
        {
            var bookList = BookList.OrderBy(x => x.Id).ToList<Book>();
            return bookList;
        }

        [HttpGet("{id}")]
        public Book GetById(int id)
        {
            var book = BookList.Where(book => book.Id == id).SingleOrDefault();
            return book;
        }

        //[HttpGet]
        //public Book GetById([FromQuery] string id)
        //{
        //    var book = BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
        //    return book;
        //}

        [HttpPost]
        public IActionResult Add([FromBody] Book newBook)
        {
            var book = BookList.SingleOrDefault(book => book.Tite == newBook.Tite);
            if (book is not null)
                return BadRequest("Böyle bir kitap zaten mevcut.");
            BookList.Add(newBook);
            return Ok("Başarılı.");
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Book updatedBook)
        {
            var book = BookList.SingleOrDefault(book => book.Id == id);
            if (book is null)
                return BadRequest("Böyle bir kitap yok.");

            book.Tite = updatedBook.Tite != default ? updatedBook.Tite : book.Tite;
            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
            return Ok("Kitap güncellendi");

        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = BookList.SingleOrDefault(book => book.Id == id);
            if (book is null)
                return BadRequest("Böyle bir kitap bulunamadı");
            BookList.Remove(book);
            return Ok("Kitap Silindi.");
        }
    }
}    

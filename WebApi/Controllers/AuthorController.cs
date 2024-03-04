using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.DbOperations;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public AuthorController(BookStoreDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAuthors()
        {
            GetAuthorsQuery getAuthorsQuery = new GetAuthorsQuery(_context,_mapper);
            var result = getAuthorsQuery.GetAuthors();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetAuthorDetailQuery getAuthorDetailQuery = new GetAuthorDetailQuery(_context,_mapper);
            getAuthorDetailQuery.AuthorId = id;
            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            validator.ValidateAndThrow(getAuthorDetailQuery);
            var result = getAuthorDetailQuery.Handle();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Add([FromBody] CreateAuthorModel newAuthor)
        {
            var id = _context.Authors.SingleOrDefault(x => x.BookId == newAuthor.BookId);
            if (id is not null)
                return BadRequest("Kitap başka yazarın üzerine kayıtlı.");
            var controlId2 = _context.Books.SingleOrDefault(x => x.Id == newAuthor.BookId);
            if(controlId2 is null)
                return BadRequest("Böyle bir kitap bulunamadı");
            CreateAuthorCommand createAuthorCommand = new CreateAuthorCommand(_context,_mapper);
            createAuthorCommand.Model = newAuthor;
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            validator.ValidateAndThrow(createAuthorCommand);
            createAuthorCommand.Handle();
            return Ok(newAuthor.Name + " başarılı bir şekilde eklendi");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateAuthorModel updateAuthorModel)
        {
            UpdateAuthorCommand updateAuthorCommand = new UpdateAuthorCommand(_context);
            updateAuthorCommand.Model = updateAuthorModel;
            updateAuthorCommand.AuthorId = id;
            UpdateAuthorCommandValidator validtor = new UpdateAuthorCommandValidator();
            validtor.ValidateAndThrow(updateAuthorCommand);
            updateAuthorCommand.Handle();
            return Ok("Kitap başarılı bir şekilde güncellendi");

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            DeleteAuthorCommand deleteAuthorCommand = new DeleteAuthorCommand(_context,_mapper);
            deleteAuthorCommand.AuthorId = id;
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            validator.ValidateAndThrow(deleteAuthorCommand);
            deleteAuthorCommand.Handle();
            return Ok("Yazar silindi");

        }
    }
}

using FluentValidation;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;

namespace WebApi.BookOperations.CreateBook
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookModel>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(command => command.GenreId).GreaterThan(0);
            RuleFor(command => command.PageCount).GreaterThan(0);
            RuleFor(command => command.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(command => command.Title).NotNull();


        }
    }
}

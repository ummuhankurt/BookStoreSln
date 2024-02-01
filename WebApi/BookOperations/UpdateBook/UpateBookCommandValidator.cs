using FluentValidation;

namespace WebApi.BookOperations.UpdateBook
{
    public class UpateBookCommandValidator : AbstractValidator<UpdateBookModel>
    {
        public UpateBookCommandValidator()
        {
            RuleFor(command => command.Title).NotEmpty().MinimumLength(1);
            RuleFor(command => command.GenreId).NotEmpty();
        }
    }
}

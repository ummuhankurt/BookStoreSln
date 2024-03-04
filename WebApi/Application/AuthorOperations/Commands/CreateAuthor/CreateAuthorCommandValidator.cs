using FluentValidation;
using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(x => x.Model.Name).NotEmpty();
            RuleFor(x => x.Model.Surname).NotEmpty();
            RuleFor(x => x.Model.BookId).NotEmpty();
            RuleFor(x => x.Model.BookId).GreaterThan(0);
            RuleFor(x => x.Model.DateOfBirth).NotEmpty();
        }
    }
}

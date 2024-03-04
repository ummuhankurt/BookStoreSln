using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
           
            RuleFor(x => x.Model.Name).NotEmpty();
            RuleFor(x => x.Model.Name).MinimumLength(1);
            RuleFor(x => x.Model.Surname).NotEmpty();
            RuleFor(x => x.Model.Surname).MinimumLength(1);
            RuleFor(x=> x.Model.DateOfBirth).NotEmpty();
            RuleFor(x => x.Model.BookId).NotEmpty();
        }
    }
}

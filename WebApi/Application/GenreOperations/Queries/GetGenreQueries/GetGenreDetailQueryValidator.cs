using FluentValidation;

namespace WebApi.Application.GenreOperations.Queries.GetGenreQueries
{
    public class GetGenreDetailQueryValidator : AbstractValidator<GetGenreDetailQuery>
    {
        public GetGenreDetailQueryValidator()
        {
            RuleFor(x => x.GenreId).NotEmpty();
            RuleFor(x => x.GenreId).GreaterThan(0);
        }
    }
}

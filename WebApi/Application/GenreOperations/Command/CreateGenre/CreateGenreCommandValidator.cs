using System;
using FluentValidation;

namespace WebApi.Application.GenreOperations.CreateGenre
{
    public class CreateGenrecommandQueryValidator : AbstractValidator<CreateGenreCommand>
    {
        //constructor
        public CreateGenrecommandQueryValidator()
        {
             RuleFor(q=> q.Model.Name).NotEmpty().MinimumLength(4);

            
        }

    }

}
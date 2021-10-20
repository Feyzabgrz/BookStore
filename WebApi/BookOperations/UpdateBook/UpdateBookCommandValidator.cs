using System;
using FluentValidation;

namespace WebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        //constructor
        public UpdateBookCommandValidator()
        {
             RuleFor(u=> u.BookId).GreaterThan(0);
             RuleFor(u=> u.Model.GenreId).GreaterThan(0);
             RuleFor(u=> u.Model.Title).NotEmpty().MinimumLength(4);


            
        }

    }

}
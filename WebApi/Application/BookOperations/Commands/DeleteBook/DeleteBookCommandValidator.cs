using System;
using FluentValidation;

namespace WebApi.BookOperations.DeleteBook
{
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
        //constructor
        public DeleteBookCommandValidator()
        {
            //Not equal - değer girebileceği anlamına gelir //0 olamaz
             RuleFor(command=> command.BookId).GreaterThan(0);
            



        }

    }

}
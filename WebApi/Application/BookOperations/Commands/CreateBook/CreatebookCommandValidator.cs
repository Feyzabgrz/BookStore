using System;
using FluentValidation;

namespace WebApi.BookOperations.CreateBook
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        //constructor
        public CreateBookCommandValidator()
        {
            //Not equal - değer girebileceği anlamına gelir //0 olamaz
             RuleFor(command=> command.Model.GenreId).GreaterThan(0);
             RuleFor(command=> command.Model.PageCount).GreaterThan(0);
             RuleFor(command=> command.Model.PublishDate).NotEmpty().LessThan(DateTime.Now.Date); //Bugünden küçük olmalı
             RuleFor(command=> command.Model.Title).NotEmpty().MinimumLength(4);



        }

    }

}
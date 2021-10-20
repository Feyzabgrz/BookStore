using System;
using FluentValidation;

namespace WebApi.BookOperations.GetBookDetail
{
    public class GetBookDetailQueryValidator : AbstractValidator<GetBookDetailQuery>
    {
        //constructor
        public GetBookDetailQueryValidator()
        {
             RuleFor(q=> q.BookId).GreaterThan(0);

            
        }

    }

}
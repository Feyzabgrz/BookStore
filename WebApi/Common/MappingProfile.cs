
using AutoMapper;
using WebApi.Entities;
using static WebApi.Application.GenreOperations.Queries.GetGenres.GetGenresQuery;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.BookOperations.GetBookDetail.GetBookDetailQuery;
using static WebApi.BookOperations.GetBooks.GetBooksQuery;

namespace WebApi.Common
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            //İlk parametre source,2. parametre target yani 1. 2. ye dönüştürülsün
            CreateMap<CreateBookModel,Book>(); //CreateBookModel objesi Book objesine maplenebilir olsun

            //Burda ihtiyaç duyaağımız bazı satırları map etmesini isteyeceğiz
            CreateMap<Book,BookDetailViewModel>().ForMember(dest=>dest.Genre, opt=>opt.MapFrom(src=>((GenreEnum)src.GenreId).ToString()));

            CreateMap<Book,BooksViewModel>().ForMember(dest=>dest.Genre, opt=>opt.MapFrom(src=>((GenreEnum)src.GenreId).ToString()));

            CreateMap<Genre,GenresViewModel>();


        }

    }
}

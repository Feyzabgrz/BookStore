using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using WebApi.DbOperations;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.CreateBook;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using WebApi.BookOperations.GetBookDetail;
using static WebApi.BookOperations.GetBookDetail.GetBookDetailQuery;
using WebApi.BookOperations.UpdateBook;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;
using WebApi.BookOperations.DeleteBook;
using AutoMapper;
using FluentValidation.Results;
using FluentValidation;

namespace WebApi.AddControllers{

    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {

        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;


        public BookController(BookStoreDbContext context,IMapper mapper)
        {
            _context=context;
            _mapper= mapper;
        }
        // private static List<Book> BookList =new List<Book>()
        // {
        //     new Book{
        //         Id=1,
        //         Title="Lean Startup", //Personel Growth
        //         GenreId=1,
        //         PageCount=200,
        //         PublishDate= new DateTime(2001,06,12)
        //     },
        //      new Book{
        //         Id=2,
        //         Title="Herland", //science Fiction
        //         GenreId=2,
        //         PageCount=250,
        //         PublishDate= new DateTime(2010,05,23)
        //     },
        //       new Book{
        //         Id=3,
        //         Title="Dune", //science Fiction
        //         GenreId=2,
        //         PageCount=540,
        //         PublishDate= new DateTime(2002,12,21)
        //     }

        // };

        [HttpGet]
        public IActionResult  GetBooks() //Burdan List<Book> dönüş tipini kaldırdık çünkü artık queryden alıyoruz
        {

            //objeyle birlikte geri döndürmek istediğimizde IActionResult tipini kullanmamız gerekiyor.
           GetBooksQuery query=new GetBooksQuery(_context,_mapper);
           var result =query.Handle();
           return Ok(result);
            

        }

        
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            BookDetailViewModel result;
            try 
            {
                GetBookDetailQuery query= new GetBookDetailQuery(_context,_mapper);
                query.BookId=id;
               GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
                 validator.ValidateAndThrow(query);
            result=query.Handle(); 
            }
            
            catch(Exception ex)
            {
              return BadRequest(ex.Message);
            }
    
            return Ok(result);

        }

        //  [HttpGet]
        // public Book Get( [FromQuery]string id)
        // {
        //     var book= BookList.Where(book=>book.Id==Convert.ToInt32(id)).SingleOrDefault();
        //     return book;
            

        // }

        //Post
          [HttpPost]
          public IActionResult AddBook([FromBody] CreateBookModel newBook)
          {
              CreateBookCommand command=new CreateBookCommand(_context,_mapper);
              try
              {
                command.Model= newBook;
                CreateBookCommandValidator validator=new CreateBookCommandValidator();
                validator.ValidateAndThrow(command);
                    command.Handle();


                // if(!result.IsValid)
                // foreach(var item in result.Errors)
                // {
                //     Console.WriteLine("Özellik"+item.PropertyName+"Error Message :"+ item.ErrorMessage)

                // }
                // else
              }
              catch(Exception ex)
              {
                 return BadRequest(ex.Message);
              }
             
              return Ok();

          }

        //Put

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody] UpdateBookModel updatedBook)
        {

            try
            {
               UpdateBookCommand command=new UpdateBookCommand(_context);
               command.BookId=id;
               command.Model=updatedBook;

                UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
                 validator.ValidateAndThrow(command);
               command.Handle();
            }
            catch(Exception ex)
            {
                 return BadRequest(ex.Message);
            }
            _context.SaveChanges();

            return Ok();

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {

            try
            {
                 DeleteBookCommand command=new DeleteBookCommand(_context);
                 command.BookId=id;
                 DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                 validator.ValidateAndThrow(command);
                 command.Handle();
            }
            catch(Exception ex)
            {
               return BadRequest(ex.Message);
            }
          

            return Ok();
        }



    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {
       // public CreateBookModel Model { get; set; }  //Bu modeli buraya set etmeliyimki buraya dolu bir şekilde gelsin
        private readonly BookStoreDbContext _dbContext;
        public int BookId { get; set; }

        public GetBookDetailQuery(BookStoreDbContext dbContext)
        {
            _dbContext=dbContext;
        }

        public BookDetailViewModel Handle()
        {
            var book= _dbContext.Books.Where(book=>book.Id==BookId).SingleOrDefault();
            if( book is null)
           throw new InvalidOperationException("Kitap Bulunamadı!");

            BookDetailViewModel vm =new BookDetailViewModel();  //Bu book u view modele map etmem gerekiyor onun içinde öncelikle nesnesini oluşturuyorum
            vm.Title=book.Title;
            vm.Genre= ((GenreEnum)book.GenreId).ToString();
            vm.PageCount=book.PageCount;
            vm.PublishDate=book.PublishDate.Date.ToString("dd/MM/yyyy");
           
            return vm;

        }

        public class BookDetailViewModel
        {
            public string Title { get; set; }
            public string  Genre { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
        }
    }       
}
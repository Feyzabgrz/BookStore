using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        
        public CreateBookModel Model { get; set; }  //Bu modeli buraya set etmeliyimki buraya dolu bir şekilde gelsin
        private readonly BookStoreDbContext _dbContext;

        public CreateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext=dbContext;
        }

        public void Handle()
        {
             var book = _dbContext.Books.SingleOrDefault(x=>x.Title == Model.Title);  //burası artık new book değil yukarıda set ettiğimiz modelden gelen title
              if (book != null)
              {
                  throw new InvalidOperationException("Kitap zaten mevcut");
              }

              //Book entitysinden yeni bir book nesnesi yaratıp bunun fieldlarını
              //dışarıdan bir model aldık,benim contextim bir entity alıyo bu entity yaratıp daha sonra   onun fieldlarını gelen model içerisinden set ediyo olmam lazım
              book=new Book();
              book.Title=Model.Title;
              book.PageCount=Model.PageCount;
              book.PublishDate=Model.PublishDate;
              book.GenreId=Model.GenreId;

              _dbContext.Books.Add(book);
              _dbContext.SaveChanges(); 

        }

        public class CreateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
        }
    }
    
}

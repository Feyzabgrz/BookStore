using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.BookOperations.CreateBook
{
    public class CreateBookCommand
    {

        public CreateBookModel Model { get; set; }  //Bu modeli buraya set etmeliyimki buraya dolu bir şekilde gelsin
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;


        public CreateBookCommand(BookStoreDbContext dbContext,IMapper mapper)
        {
            _dbContext=dbContext;
            _mapper=mapper;
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


              book= _mapper.Map<Book>(Model) ;   //new Book();   //Buda demek ki model ile gelen veriti book nesnesine convert et
            //   book.Title=Model.Title;         //Artık buraya ihtiyacımız olmayacak çünkü map edeceğiz mappingprofile sınıfından
            //   book.PageCount=Model.PageCount;
            //   book.PublishDate=Model.PublishDate;
            //   book.GenreId=Model.GenreId;

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

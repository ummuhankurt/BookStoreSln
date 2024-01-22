﻿using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entity;

namespace WebApi.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public CreateBookModel Model { get; set; }
        private readonly IMapper _mapper;
        public CreateBookCommand(BookStoreDbContext bookStoreDbContext, IMapper mapper)
        {
            _dbContext = bookStoreDbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);
            if (book is not null)
                throw new InvalidOperationException("Kitap zaten mevcut");

            book = _mapper.Map<Book>(Model);
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

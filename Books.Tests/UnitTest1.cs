using Books.Common.Models;
using ServiceLayer.services;
using System;
using System.Collections.Generic;
using Xunit;

namespace Books.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test_GetAllBooks_When_No_Books()
        {
            BookService bookService = new BookService();

            var actualResponse = bookService.GetAllBooks();

            Assert.Empty((IEnumerable<Book>)actualResponse.Result);
        }

        [Fact]
        public void Test_GetAllBooks_When_Populated()
        {
            BookService bookService = new BookService();

            var book1 = new Book() { Id = 1, Name = "Hello", Author = "Chaitanya", Category = "Fiction", Price = 500 };
            var book2 = new Book() { Id = 2, Name = "Without Conscience", Author = "Doyle", Category = "Biography", Price = 600 };
            List< Book> books = new List<Book>()
            {
                book1,
                book2
            };

            bookService.AddBook(book1);
            bookService.AddBook(book2);

            var actualResponse = bookService.GetAllBooks();

            Assert.True(actualResponse.Status);
            Assert.Equal(books, actualResponse.Result);
        }


        [Fact]
        public void Test_GetBookById_Negative_Number()
        {
            BookService bookService = new BookService();

            var actualResponse = bookService.GetBookById(-1);

            Assert.Null(actualResponse.Result);
            Assert.False(actualResponse.Status);
            Assert.Equal("Invalid Id, Id should be a positive number.", actualResponse.Error);
        }


        [Fact]
        public void Test_GetBookById_NonExistent_Id()
        {
            BookService bookService = new BookService();

            var actualResponse = bookService.GetBookById(1);

            Assert.Null(actualResponse.Result);
            Assert.False(actualResponse.Status);
            Assert.Equal("No Book exists with given id", actualResponse.Error);
        }


        [Fact]
        public void Test_GetBookById_Existent_Id()
        {
            BookService bookService = new BookService();

            var book1 = new Book() { Id = 1, Name = "Hello", Author = "Chaitanya", Category = "Fiction", Price = 500 };
            var book2 = new Book() { Id = 2, Name = "Without Conscience", Author = "Doyle", Category = "Biography", Price = 600 };
            List<Book> books = new List<Book>()
            {
                book1,
                book2
            };

            bookService.AddBook(book1);
            bookService.AddBook(book2);

            var actualResponse = bookService.GetBookById(1);

            Assert.Equal(book1, actualResponse.Result);
            Assert.True(actualResponse.Status);
        }


        [Fact]
        public void Test_AddBook_Negative_Id()
        {
            BookService bookService = new BookService();

            var book = new Book() { Id = -1, Name = "Hello", Author = "Chaitanya", Category = "Fiction", Price = 500 };
            var actualResponse = bookService.AddBook(book);

            Assert.Null(actualResponse.Result);
            Assert.False(actualResponse.Status);
            Assert.Equal("Invalid data.", actualResponse.Error);
        }


        [Fact]
        public void Test_AddBook_Negative_Price()
        {
            BookService bookService = new BookService();

            var book = new Book() { Id = 1, Name = "Hello", Author = "Chaitanya", Category = "Fiction", Price = -500 };
            var actualResponse = bookService.AddBook(book);

            Assert.Null(actualResponse.Result);
            Assert.False(actualResponse.Status);
            Assert.Equal("Invalid data.", actualResponse.Error);
        }


        [Theory]
        [InlineData("Hello123")]
        [InlineData("12345")]
        [InlineData("Hello*(")]
        public void Test_InValid_BookNames(string bookName)
        {
            BookService bookService = new BookService();

            var book = new Book() { Id = 1, Name = bookName, Author = "Chaitanya", Category = "Fiction", Price = 500 };
            var actualResponse = bookService.AddBook(book);

            Assert.Null(actualResponse.Result);
            Assert.False(actualResponse.Status);
            Assert.Equal("Invalid data.", actualResponse.Error);
        }


        [Theory]
        [InlineData("Hello")]
        [InlineData("Without Conscience")]
        [InlineData("Rich Dad. Poor Dad")]
        public void Test_Valid_BookNames(string bookName)
        {
            BookService bookService = new BookService();

            var book = new Book() { Id = 1, Name = bookName, Author = "Chaitanya", Category = "Fiction", Price = 500 };
            var actualResponse = bookService.AddBook(book);

            Assert.True(actualResponse.Status);
        }


        [Theory]
        [InlineData("Varun123")]
        [InlineData("12345")]
        [InlineData("Rahul/Kumar")]
        public void Test_InValid_AuthorNames(string authorName)
        {
            BookService bookService = new BookService();

            var book = new Book() { Id = 1, Name = "Without Conscience", Author = authorName, Category = "Fiction", Price = 500 };
            var actualResponse = bookService.AddBook(book);

            Assert.Null(actualResponse.Result);
            Assert.False(actualResponse.Status);
            Assert.Equal("Invalid data.", actualResponse.Error);
        }


        [Theory]
        [InlineData("Robert")]
        [InlineData("Richard Dwells")]
        [InlineData("J.k.Rowling")]
        public void Test_Valid_AuthorNames(string authorName)
        {
            BookService bookService = new BookService();

            var book = new Book() { Id = 1, Name = "Without", Author = authorName, Category = "Fiction", Price = 500 };
            var actualResponse = bookService.AddBook(book);

            Assert.True(actualResponse.Status);
        }


        [Fact]
        public void Test_AddBook_ValidData()
        {
            BookService bookService = new BookService();

            var book = new Book() { Id = 1, Name = "Hello", Author = "Chaitanya", Category = "Fiction", Price = 500 };
            var actualResponse = bookService.AddBook(book);

            Assert.True(actualResponse.Status);
        }

        [Fact] 
        public void Test_UpdateBook_InValidData()
        {
            BookService bookService = new BookService();

            var book = new Book() { Id = 1, Name = "Hello", Author = "Chaitanya", Category = "Fiction", Price = 500 };
            bookService.AddBook(book);

            var newBook = new Book() { Id = -1, Name = "Hell(o", Author = "Chai*tanya", Category = "Fiction/", Price = -500 };
            var actualResponse = bookService.UpdateBook(newBook);

            Assert.Null(actualResponse.Result);
            Assert.False(actualResponse.Status);
            Assert.Equal("Invalid data.", actualResponse.Error);
        }


        [Fact]
        public void Test_UpdateBook_ValidData()
        {
            BookService bookService = new BookService();

            var oldBook = new Book() { Id = 1, Name = "Hello", Author = "Chaitanya", Category = "Fiction", Price = 500 };
            bookService.AddBook(oldBook);

            var newBook = new Book() { Id = 1, Name = "Hello World", Author = "Chaitanya Bammidi", Category = "Fiction", Price = 600 };
            var actualResponse = bookService.UpdateBook(newBook);

            Assert.True(actualResponse.Status);
            Assert.Equal(bookService.GetBookById(oldBook.Id).Result, bookService.GetBookById(oldBook.Id).Result);
        }


        [Fact]
        public void Test_DeleteBook_NonExistentBook()
        {
            BookService bookService = new BookService();

            var book = new Book() { Id = 1, Name = "Hello", Author = "Chaitanya", Category = "Fiction", Price = 500 };

            var actualResponse = bookService.DeleteBook(book);

            Assert.False(actualResponse.Status);
        }


        [Fact]
        public void Test_DeleteBook_ExistentBook()
        {
            BookService bookService = new BookService();

            var book1 = new Book() { Id = 1, Name = "Hello", Author = "Chaitanya", Category = "Fiction", Price = 500 };
            var book2 = new Book() { Id = 2, Name = "Without Conscience", Author = "Doyle", Category = "Biography", Price = 600 };
            List<Book> books = new List<Book>()
            {
                book1,
                book2
            };

            bookService.AddBook(book1);
            bookService.AddBook(book2);
            var actualResponse = bookService.DeleteBook(book1);

            Assert.True(actualResponse.Status);
            Assert.Equal(new List<Book> { book2 }, bookService.GetAllBooks().Result);

        }
    }
}

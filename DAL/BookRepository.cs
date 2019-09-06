using System;
using System.Collections.Generic;
using Books.Common.Models;

namespace DAL
{
    public class BookRepository
    {
        private List<Book> _books;
        public BookRepository()
        {
            _books = new List<Book>();
        }


        public List<Book> GetAllBooks()
        {
            return _books;
        }


        public Book GetBookById(int id)
        {
            return _books.Find(book => book.Id == id);
        }


        public bool AddBook(Book book)
        {
            try
            {
                _books.Add(book);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool UpdateBook(Book book)
        {
            try
            {
                var oldBook = _books.Find(b => b.Id == book.Id);
                oldBook.Name = book.Name;
                oldBook.Author = book.Author;
                oldBook.Category = book.Category;
                oldBook.Price = book.Price;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool DeleteBook(Book book)
        {
            try
            {
                var requiredBook = _books.Find(b => b.Id == book.Id);
                if (requiredBook == null)
                    return false;
                _books.Remove(requiredBook);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}

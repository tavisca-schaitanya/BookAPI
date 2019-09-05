using Books.Common.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.services;

namespace Books.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }


        [HttpGet("GetAllBooks")]
        public JsonResult GetAllBooks()
        {
            return new JsonResult(_bookService.GetAllBooks());
        }


        [HttpGet("GetBookById")]
        public JsonResult GetBookById(int id)
        {
            return new JsonResult(_bookService.GetBookById(id));
        }        


        [HttpPost("AddBook")]
        public JsonResult AddBook(Book book)
        {
            return new JsonResult(_bookService.AddBook(book));
        }


        [HttpPut("UpdateBook")]
        public JsonResult UpdateBook(Book book)
        {
            return new JsonResult(_bookService.UpdateBook(book));
        }


        [HttpDelete("DeleteBook")]
        public JsonResult DeleteBook(Book book)
        {
            return new JsonResult(_bookService.DeleteBook(book));
        }


    }
}

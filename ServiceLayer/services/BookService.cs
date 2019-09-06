using Books.Common.Models;
using DAL;
using System.Text.RegularExpressions;

namespace ServiceLayer.services
{
    public class BookService: IService
    {

        private BookRepository _bookRepository;
        public BookService()
        {
            _bookRepository = new BookRepository();
        }


        public Response CreateResponse(object result, string error = "", bool status = true)
        {
            Response response = new Response();
            response.Result = result;
            response.Error = error;
            response.Status = status;
            return response;

        }


        public Response Get()
        {
            return CreateResponse(_bookRepository.GetAllBooks());
        }


        public Response Get(int id)
        {
            if (id <= 0)
                return CreateResponse(null, "Invalid Id, Id should be a positive number.", false);

            var book = _bookRepository.GetBookById(id);

            if (book == null)
                return CreateResponse(null, "No Book exists with given id", false);

            return CreateResponse(book);
        }


        //Validates string should contains alphabets, spaces and period
        public bool ValidateNames(params string[] attributes)
        {
            for (int i = 0; i < attributes.Length; i++)
                if (!Regex.IsMatch(attributes[i], @"^[a-zA-Z. ]+$"))
                    return false;
            return true;
        }


        public bool ValidateBook(Book book)
        {
            if (book.Id <= 0 || book.Price <= 0)
                return false;

            return ValidateNames(book.Name, book.Category, book.Author);
        }


        public Response Post(Book book)
        {
            if (!ValidateBook(book))
                return CreateResponse(null, "Invalid data.", false);

            return CreateResponse("", "", _bookRepository.AddBook(book));
        }


        public Response Put(Book book)
        {
            if (!ValidateBook(book))
                return CreateResponse(null, "Invalid data.", false);

            return CreateResponse("", "", _bookRepository.UpdateBook(book));
        }


        public Response Delete(Book book)
        {
            return CreateResponse("", "", _bookRepository.DeleteBook(book));
        }
    }
}

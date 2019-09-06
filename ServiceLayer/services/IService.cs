using Books.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.services
{
    interface IService
    {
        Response Get();
        Response Get(int id);
        Response Post(Book book);
        Response Put(Book book);
        Response Delete(Book book);
    }
}

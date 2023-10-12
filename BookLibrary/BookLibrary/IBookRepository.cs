using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary
{
    public interface IBookRepository
    {
        Book Add(Book book);
        IEnumerable<Book> Get(string? title = null, double? price = null, string? orderBy = null);
        Book? GetById(int id);
        Book? DeleteById(int id);
        Book? Update(int id, Book updatedBook);


    }
}

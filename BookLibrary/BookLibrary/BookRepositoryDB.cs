using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary
{
    public class BookRepositoryDB
    {
        private readonly BookDBContext _context;


        public BookRepositoryDB(BookDBContext dbContext)
        {
            _context = dbContext;
        }


        //Validate, through the validate methods made in Book.cs, assign default ID (0), add the book to the List (Books)
        //And finally return the Book object (In case of further use later on).
        public Book Add(Book book)
        {
            book.Validate();
            book.ID = 0;
            _context.Books.Add(book);
            _context.SaveChanges();
            return book;
        }



        //Order function. Allows for the retrieval of Books from Title and Price. orderBy is a string 
        // - that is used to identify which filter is used. I.E.
        public IEnumerable<Book> Get(string? titleIncludes = null, int? price = null, string? orderBy = null)
        {            
            IQueryable<Book> query = _context.Books.AsQueryable();

            if (titleIncludes != null)
            {
                query = query.Where(m => m.Title.Contains(titleIncludes));
            }
            if (orderBy != null)
            {
                orderBy = orderBy.ToLower();
                switch (orderBy)
                {
                    case "title":
                    case "title_asc":
                        query = query.OrderBy(m => m.Title);
                        break;
                    case "title_desc":
                        query = query.OrderByDescending(m => m.Title);
                        break;
                    case "price":
                    case "price_asc":
                        query = query.OrderBy(m => m.Price);
                        break;
                    case "price_desc":
                        query = query.OrderByDescending(m => m.Price);
                        break;

                    default:
                        break; // do nothing
                        //throw new ArgumentException("Unknown sort order: " + orderBy);
                }
            }
            return query;
        }


        //simple GetByID function. Returns the object book from the list Books with the given ID in argument.
        public Book? GetById(int id)
        {
            return _context.Books.FirstOrDefault(m => m.ID == id);
        }


        //Removes book object from the List Books by ID given in argument and returns the deleted in object
        //in case of further use. 
        public Book? Remove(int id)
        {
            Book? book = GetById(id);
            if (book is null)
            {
                return null;
            }
            _context.Books.Remove(book);
            _context.SaveChanges();
            return book;
        }


        //
        public Book? Update(int id, Book book)
        {
            book.Validate();
            Book? bookToUpdate = GetById(id);
            if (bookToUpdate == null) return null;
            bookToUpdate.Title = book.Title;
            bookToUpdate.Price = book.Price;
            _context.SaveChanges();
            return bookToUpdate;
        }

    }
}

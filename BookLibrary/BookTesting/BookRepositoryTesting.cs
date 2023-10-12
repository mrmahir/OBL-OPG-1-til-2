using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BookLibrary;
using Microsoft.SqlServer;

namespace BookTesting
{

    [TestClass]
    public class BookRepositoryDBTests
    {
        private DbContextOptions<BookDBContext> _options;
        private BookDBContext _context;
        private BookRepositoryDB _repository;

        [TestInitialize]
        public void Setup()
        {
            // Set up the in-memory database
            _options = new DbContextOptionsBuilder<BookDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new BookDBContext(_options);
            _repository = new BookRepositoryDB(_context);
        }

        [TestMethod]
        public void Add_BookIsValid_AddsBookToDatabase()
        {
            // Arrange
            var book = new Book { Title = "Test Book", Price = 20 };

            // Act
            var addedBook = _repository.Add(book);

            // Assert
            Assert.IsNotNull(addedBook.ID);
            Assert.AreEqual(book.Title, addedBook.Title);
            Assert.AreEqual(book.Price, addedBook.Price);
        }


        [TestMethod]
        public void Remove_BookExists_RemovesBookFromDatabase()
        {
            // Arrange
            var book = new Book { Title = "Test Book", Price = 20 };
            _context.Books.Add(book);
            _context.SaveChanges();

            // Act
            var removedBook = _repository.Remove(book.ID);

            // Assert
            Assert.IsNotNull(removedBook);
            Assert.AreEqual(book.ID, removedBook.ID);

            // Verify that the book was removed from the database
            var bookInDatabase = _context.Books.Find(book.ID);
            Assert.IsNull(bookInDatabase);
        }

        [TestMethod]
        public void Remove_BookDoesNotExist_ReturnsNull()
        {
            // Arrange
            var nonExistentBookId = 999; 

            // Act
            var result = _repository.Remove(nonExistentBookId);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetById_BookExists_ReturnsBook()
        {
            // Arrange
            var book = new Book { Title = "Test Book", Price = 20 };
            _context.Books.Add(book);
            _context.SaveChanges();

            // Act
            var result = _repository.GetById(book.ID);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(book.ID, result.ID);
            Assert.AreEqual(book.Title, result.Title);
            Assert.AreEqual(book.Price, result.Price);
        }

        [TestMethod]
        public void GetById_BookDoesNotExist_ReturnsNull()
        {
            // Arrange
            var nonExistentBookId = 999;

            // Act
            var result = _repository.GetById(nonExistentBookId);

            // Assert
            Assert.IsNull(result);
        }




    }

}

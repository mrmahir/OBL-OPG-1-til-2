using BookLibrary;

namespace BookTesting
{




    [TestClass]
    public class BookTesting
    {      
        //Book title errors
        Book shortNameBook = new Book() { ID = 1, Title = "Pi", Price = 200};
        Book nullNameBook = new Book() { ID = 6, Price = 100 };
        //Book price errors
        Book highPriceBook = new Book() { ID = 2, Title = "The Lord of The Rings", Price = 3000 };
        Book lowPriceBook = new Book() { ID = 3, Title = "Harry Potter", Price = -200};
        //Book all errors
        Book everythingWrongBook = new Book() { ID = 4, Title = "GT", Price = 2500 };
        //Book no errors
        Book book = new Book() { ID = 5, Title = "A Song of Ice and Fire", Price = 250 };

        [TestMethod]

        public void ValidateBookTitle()
        {
            book.ValidateTitle();

            Assert.ThrowsException<ArgumentException>(() => shortNameBook.ValidateTitle());          
        }

        [TestMethod]
        public void ValidateBookPrice()
        {
            book.ValidatePrice();

            Assert.ThrowsException<ArgumentException>(() => highPriceBook.ValidatePrice());
            Assert.ThrowsException<ArgumentException>(() => lowPriceBook.ValidatePrice());
        }

        [TestMethod]
        public void ValidateEverything()
        {
            book.Validate();

            Assert.ThrowsException<ArgumentException>(() => everythingWrongBook.Validate());
        }
        [TestMethod]
        public void ValidateValidForm()
        {
            book.Validate();
        }

        public void TestMethod1()
        {         
        }
    }
}
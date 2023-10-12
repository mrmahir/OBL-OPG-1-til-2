namespace BookLibrary
{
    public class Book
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }



        public override string ToString()
        {
            return $"ID: {ID}, Title: {Title}, Price: {Price}.";
        }


        public void ValidateTitle()
        {
            if (Title.Length < 3)
            {
                throw new ArgumentException("Title must be at least 3 characters long");
            }

            if (Title == null)
            {
                throw new ArgumentNullException("Title cannot be null");
            }
        }

        public void ValidatePrice()
        {
            if (Price <= 0 || Price > 1200)
            {
                throw new ArgumentException("Price must be above 0 and 1200 or below");
            }
        }

        public void Validate()
        {
            ValidatePrice();
            ValidateTitle();
        }
    }
}
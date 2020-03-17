namespace Library.Domain
{
    public class BookStockChanged
    {
        public int booksAdded;

        public BookStockChanged(BookAmount amount)
        {
            this.booksAdded = amount.Amount;
        }
    }
}

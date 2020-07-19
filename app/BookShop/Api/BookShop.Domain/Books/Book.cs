using BookShop.Domain.Common;

namespace BookShop.Domain.Books
{
    public class Book : Entity
    {
        public Book(string title, int stock)
        {
            Title = title?.Trim();
            Stock = stock;
        }

        public const string TableName = "TB_BOOK";
        public const string BookIdColumnName = "BOOK_ID";
        public const string TitleColumnName = "TITLE";
        public const string StockColumnName = "STOCK";

        public int BookId { get; private set; }
        public string Title { get; private set; }
        public int Stock { get; private set; }
    }
}
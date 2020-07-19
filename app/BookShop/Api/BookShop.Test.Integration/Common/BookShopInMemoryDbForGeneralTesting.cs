using System.Collections.Generic;
using BookShop.Domain.Books;
using BookShop.Infra.Data;

namespace BookShop.Test.Integration.Common
{
    public static class BookShopInMemoryDbForGeneralTesting
    {
        public static void Initialize(BookShopDbContext context)
        {
            SeedBooks(context);
            context.SaveChanges();
        }

        public static void Reinitialize(BookShopDbContext context)
        {
            context.Set<Book>().RemoveRange(GetBooks());
            context.SaveChanges();
            Initialize(context);
        }

        public static List<Book> GetBooks()
        {
            var books = new List<Book>()
            {
                new Book("UML2 em Modelagem Orientada a Objetos", 10),
                new Book("Internet das Coisas com ESP5266, Arduino e Raspberry Pi", 2),
                new Book("Desenvolvendo para iPhone e iPad", 0)
            };

            var id = 1;
            books.ForEach(book => 
                PrivatePropertyHelper.SetPrivatePropertyValue<int>(book, "BookId", id++));

            return books;
        }

        public static void SeedBooks(BookShopDbContext context)
        {
            var books = GetBooks();

            context.Set<Book>().AddRange(books);

            context.SaveChanges();
        }
    }
}
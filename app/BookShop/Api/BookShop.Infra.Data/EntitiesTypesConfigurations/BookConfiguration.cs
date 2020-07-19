using BookShop.Domain.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.Infra.Data.EntitiesTypesConfigurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable(Book.TableName);
            builder.HasKey(k => k.BookId);
            builder.Property(p => p.BookId).HasColumnName(Book.BookIdColumnName);
            builder.Property(p => p.Title).HasColumnName(Book.TitleColumnName);
            builder.Property(p => p.Stock).HasColumnName(Book.StockColumnName);
        }
    }
}
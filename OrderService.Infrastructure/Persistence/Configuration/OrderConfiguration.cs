using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.Entities;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Status)
            .HasConversion<string>();

        builder.OwnsMany(x => x.Items, item =>
        {
            item.WithOwner()
                .HasForeignKey("OrderId");

            item.HasKey(x => x.Id);

            item.Property(x => x.ProductName)
                .HasMaxLength(200);
        });
    }
}
namespace Nest.Persistence.ModelConfigurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Description)
            .IsRequired(false)
            .HasMaxLength(5000);

        builder.Property(p => p.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.Discount)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(p => p.Quantity)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(p => p.InStock)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(p => p.SKU)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(p => p.Vendor)
            .WithMany(v => v.Products)
            .HasForeignKey(p => p.VendorId);
    }
}
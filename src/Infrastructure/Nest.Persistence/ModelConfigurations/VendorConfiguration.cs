namespace Nest.Persistence.ModelConfigurations;

public class VendorConfiguration : IEntityTypeConfiguration<Vendor>
{
    public void Configure(EntityTypeBuilder<Vendor> builder)
    {
        builder.Property(v => v.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(v => v.Email)
            .HasMaxLength(100);

        builder.Property(v => v.PhoneNumber)
            .HasMaxLength(20);

        builder.Property(v => v.Address)
            .HasMaxLength(200);

        builder.Property(v => v.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(v => v.Image)
            .HasMaxLength(200);

        builder.HasMany(v => v.Products)
            .WithOne(p => p.Vendor)
            .HasForeignKey(p => p.VendorId);
    }
}
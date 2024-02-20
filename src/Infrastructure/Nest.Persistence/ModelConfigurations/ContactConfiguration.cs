namespace Nest.Persistence.ModelConfigurations;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.Property(x => x.FullName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Phone)
            .HasMaxLength(20);

        builder.Property(x => x.Subject)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Message)
            .HasMaxLength(2500)
            .IsRequired();
    }
}
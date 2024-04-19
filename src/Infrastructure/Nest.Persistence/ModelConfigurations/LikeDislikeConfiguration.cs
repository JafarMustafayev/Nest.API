namespace Nest.Persistence.ModelConfigurations;

public class LikeDislikeConfiguration : IEntityTypeConfiguration<Likes>
{
    public void Configure(EntityTypeBuilder<Likes> builder)
    {
        builder.Property(x => x.ProductId)
            .IsRequired();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.IsLike)
            .IsRequired();
    }
}
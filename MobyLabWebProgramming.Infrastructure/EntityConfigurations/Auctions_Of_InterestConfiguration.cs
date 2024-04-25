using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

/// <summary>
/// This is the entity configuration for the UserFile entity.
/// </summary>
public class Auctions_Of_InterestConfiguration : IEntityTypeConfiguration<Auctions_Of_Interest>
{
    public void Configure(EntityTypeBuilder<Auctions_Of_Interest> builder)
    {
        builder.Property(e => e.Id)
            .IsRequired();
        builder.HasKey(x => x.Id);
        builder.Property(e => e.Participated)
            .IsRequired();

        builder.HasOne(e => e.User) // This specifies a one-to-many relation.
            .WithMany(e => e.AuctionsOfInterest) // This provides the reverse mapping for the one-to-many relation. 
            .HasForeignKey(e => e.UserId) // Here the foreign key column is specified.
            .HasPrincipalKey(e => e.Id) // This specifies the referenced key in the referenced table.
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade); // This specifies the delete behavior when the referenced entity is removed.

        builder.HasOne(e => e.Auction) // This specifies a one-to-many relation.
            .WithMany(e => e.AuctionsOfInterest) // This provides the reverse mapping for the one-to-many relation. 
            .HasForeignKey(e => e.AuctionId) // Here the foreign key column is specified.
            .HasPrincipalKey(e => e.Id) // This specifies the referenced key in the referenced table.
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade); // This specifies the delete behavior when the referenced entity is removed.

    }
}

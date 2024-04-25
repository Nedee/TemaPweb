namespace MobyLabWebProgramming.Core.Entities;

/// <summary>
/// This is an example for another entity to store files and an example for a One-To-Many relation.
/// </summary>
public class Auctions_Of_Interest : BaseEntity
{
    public Guid AuctionId { get; set; }
    public Guid UserId { get; set; }
    public int Participated { get; set; }

    // Proprietăți de navigare pentru a reprezenta relațiile între entități
    public Auctions Auction { get; set; } = default!;
    public User User { get; set; } = default!;
}